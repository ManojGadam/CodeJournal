using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;
using PersonalWebsite.Models;
using PersonalWebsite.Repository;
using PersonalWebsite.Utils;
using System;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;
using Cookie = System.Net.Cookie;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using PersonalWebsite.Views;

namespace PersonalWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : Controller
    {
        private readonly HttpClient _httpClient;
        private IProblemRepository _problemRepository;
        private readonly StackExchange.Redis.IDatabase _redis;
       // private const string leetCodeApiURL = "https://leetcode.com/graphql";
        public ProblemController(IProblemRepository problemRepository, HttpClient httpClient,IConnectionMultiplexer multiplexer)
        {
            _problemRepository = problemRepository;
            _httpClient = httpClient;
            _redis = multiplexer.GetDatabase();
        }

        [HttpGet("GetProblems")]
        public async Task<IActionResult> GetProblems()
        {
            try
            {
                var json = _redis.StringGetAsync("homePage").GetAwaiter().GetResult();
                List<ProblemView> problems;
                //Console.WriteLine(json.ToString());
                if (json.IsNullOrEmpty)
                {
                    var problem = await _problemRepository.GetProblems();
                    problems = problem.Select(x=>new ProblemView() { Comments = x.Comments, Difficulty = x.Difficulty, Id = x.Id,Link = x.Link,Name = x.Name,ProblemNumber =  x.ProblemNumber,Tags =x.Tags }).ToList();
                   var jsonStr = System.Text.Json.JsonSerializer.Serialize(problems);
                     //problems = System.Text.Json.JsonSerializer.Deserialize<List<Problem>>(jsonStr);
                    var setProblems = _redis.StringSetAsync("homePage", jsonStr);
                    var expiry = _redis.KeyExpireAsync("homePage", TimeSpan.FromMinutes(5));
                    await Task.WhenAll(setProblems, expiry);
                }
                else problems = System.Text.Json.JsonSerializer.Deserialize<List<ProblemView>>(json);
                //fix this
                return Ok(problems);
            }
            catch (RedisException ex)
            {
                Console.WriteLine($"Redis error: {ex.Message}");
                throw;
            }

        }

        [HttpPost("SaveConfiguration")]
        public async Task<IActionResult> SaveConfiguration([FromBody] ConfigCommand config)
        {
            await _problemRepository.SaveConfiguration(new Config { GitToken = config.GitToken,GitURL = config.GitURL,Id = config.Id});
            return Ok(new { isSuccessful = true });
        }
 
       
        [HttpPost("PushToGit")]
        public async Task<IActionResult> PushToGit([FromBody]GitContent content)
        {
            //check this
            var config = await GetConfiguration();
            var RepoName = config.GitURL;
            var Token = config.GitToken;
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("PersonalWebsite");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var response = await _problemRepository.GetProblem(content.Id);
            var userName = RepoName.Split('/')[3];
            var Repo = RepoName.Split('/')[4];
            var commitMessage = $"[Time Beats: {response.TimePercentile}%] [Memory Beats: {response.MemoryPercentile}%] [Solved on: {DateTime.Now:D}]";
            var title = $"{response.ProblemNumber}-{response.Name}{FileExtensionDictionary.GetExtension(response.LangName)}";
            var putUrl = $"https://api.github.com/repos/{userName}/{Repo}/contents/{title}";
            var sha = GetSha(putUrl).GetAwaiter().GetResult();
            var payload = new
            {
                message = commitMessage,
                content = Convert.ToBase64String(Encoding.UTF8.GetBytes(response.Code)),
                branch = "main",
                sha
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var putResponse = _httpClient.PutAsync(putUrl, new StringContent(jsonPayload, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            if (putResponse.IsSuccessStatusCode)
            {
                return Ok(new { isSuccessful = true });
            }
            else
            {
                var errorContent = await putResponse.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }
      
        [HttpPost("AddProblemDetails")]
        public async Task<IActionResult> AddProblemDetails([FromBody]LeetCodeProblemCommand problem)
        {
            try
            {
                var newProblem = new Problem { Code = problem.Code, Comments = problem.Comments, Id = problem.Id, Name = problem.Name, Difficulty = problem.Difficulty, Link = problem.URL, ProblemNumber = problem.ProblemNumber, Tags = problem.Tags,LangName=problem.LangName,MemoryPercentile=problem.MemoryPercentile,TimePercentile=problem.RunTimePercentile };
                var res = await _problemRepository.SaveProblem(newProblem);
                if (!res) throw new Exception("Id not found");
                await _redis.StringAppendAsync("homePage", System.Text.Json.JsonSerializer.Serialize(newProblem));
                return Ok(new { isSuccessfull = true });
            }
            catch (RedisException ex)
            {
                Console.WriteLine($"Redis error: {ex.Message}");
                throw;
            }

        }

        //Code related to prev apprach no longer necessary
        #region
        //Split into three apis
        [HttpPost("SaveProblem")]
        public async Task<IActionResult> SaveProblem([FromBody] String url)
        {
            int i = 30;
            await _redis.KeyDeleteAsync("homePage");
            StringBuilder title = new();
            while (!url[i].Equals('/'))
            {
                title.Append(url[i]);
                i++;
            }
            String titleSlug = title.ToString();
            var variables = new { titleSlug };
            var responseContent = await HandleGraphql("saveProblem", variables);
            var responseData = JObject.Parse(responseContent);
            var data = responseData["data"];
            if (data == null || !data.HasValues)
            {
                throw new Exception("Output object is empty");
            }
            var question = data["question"];
            if (question == null || !question.HasValues)
            {
                throw new Exception("Output object is empty");
            }
            var tags = await GetTags(titleSlug);
            Problem problem = new() { Name = (string)question["title"], Difficulty = (string)question["difficulty"], Link = url, Tags = String.Join(",", tags.Select(x => x.name).ToList()), ProblemNumber = (long)question["questionFrontendId"] };
            await _problemRepository.SaveProblem(problem);
            return Ok(new { isSuccessful = true });
        }
        // Delete cookie before pushing to github. Using cookie to handle the authecation need to update it
        private async Task<submissions> GetSubmissionId(string questionSlug)
        {
            var config = await GetConfiguration();
            // _httpClient.DefaultRequestHeaders.Add("Cookie", config.LeetToken);
            var variables = new
            {
                questionSlug,
                offset = 0,
                limit = 20,
                lastKey = (string?)null
            };
            var responseContent = HandleGraphql("getSubmissionId", variables).GetAwaiter().GetResult();
            var leetCodeResponse = JsonConvert.DeserializeObject<SubmissionResponse>(responseContent);
            var response = leetCodeResponse.data.questionSubmissionList.Submissions.FirstOrDefault(x => x.status == 10) ?? throw new Exception("No accepted submission");
            var code = GetCode(response.Id).GetAwaiter().GetResult();
            response.codeDetails = code;
            return response;
        }
        private async Task<CodeDetails> GetCode(long id)
        {
            //USe this to get runtime and memory
            var config = await GetConfiguration();
            //_httpClient.DefaultRequestHeaders.Add("Cookie", config.LeetToken);
            var variables = new { submissionId = id };
            var responseContent = await HandleGraphql("getCode", variables);
            var responseData = JsonConvert.DeserializeObject<CodeDetails>(responseContent);
            return responseData;
        }
        private async Task<string> HandleGraphql(string queryName, object variables)
        {
            var leetcodeApiUrl = "https://leetcode.com/graphql";
            var graphqlRequest = new
            {
                query = QueryDictionary.GetQuery(queryName),
                variables
            };
            var httpResponse = _httpClient.PostAsJsonAsync(leetcodeApiUrl, graphqlRequest, CancellationToken.None).GetAwaiter().GetResult();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Api failed");
            }
            var responseContent = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return responseContent;
        }
        private async Task<Config> GetConfiguration()
        {
            return await _problemRepository.GetConfiguration();
        }

        private async Task<List<topicTag>> GetTags(String titleSlug)
        {
            var variables = new { titleSlug };
            var responseData = await HandleGraphql("getTags", variables);
            var leetCodeResponse = JsonConvert.DeserializeObject<GraphQLResponse>(responseData);

            if (leetCodeResponse?.Data?.Question?.TopicTags == null)
            {
                throw new Exception("Output object is empty");
            }

            return leetCodeResponse.Data.Question.TopicTags;
            //return Ok(topicTags);
        }
        private async Task<string?> GetSha(string url)
        {
            var getResponse = await _httpClient.GetAsync(url);
            var fileInfo = await getResponse.Content.ReadAsStringAsync();
            var fileJson = JsonConvert.DeserializeObject<dynamic>(fileInfo);
            return fileJson?.sha;
        }
        [HttpPost("AddProblem")]
        public async Task<IActionResult> AddProblem([FromBody] Problem problem)
        {
            try
            {
                await _redis.KeyDeleteAsync("homePage");
                Boolean res = await _problemRepository.SaveProblem(problem);
                if (!res) throw new Exception("Id not found");
                return Ok(new { isSuccessfull = true });
            }
            catch (RedisException ex)
            {
                Console.WriteLine($"Redis error: {ex.Message}");
                throw;
            }

        }
        #endregion

    }
}
