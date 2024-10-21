using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalWebsite.Context;
using PersonalWebsite.Models;


namespace PersonalWebsite.Repository
{
        
    public interface IProblemRepository
    {
       Task<Boolean> SaveProblem(Problem problem);
        Task<List<Problem>> GetProblems();
        Task SaveConfiguration(Config config);
        Task<Config> GetConfiguration();
    }
    public class ProblemRepository : IProblemRepository
    {
        private ProblemContext _context;
        public ProblemRepository(ProblemContext context)
        {
            _context = context;
        }

        public async Task<Boolean> SaveProblem(Problem problem)
        {
            if (problem.Id != 0)
            {
               var currProblem = await _context.Problems.Where((x) => x.Id == problem.Id).FirstOrDefaultAsync();
                if (currProblem == null) return false;
                currProblem.Comments = problem.Comments;
                await _context.SaveChangesAsync();
                return true;
            }
            _context.Problems.Add(problem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Problem>> GetProblems()
        {
            var problems  = await _context.Problems.ToListAsync();
            return problems;
        }
        public async Task SaveConfiguration(Config config)
        {
            var configAv = await _context.Config.FirstOrDefaultAsync();
            if (configAv!=null)
            {
                configAv.LeetToken = config.LeetToken;
                configAv.GitURL = config.GitURL;
                config.GitToken = configAv.GitToken;
                await _context.SaveChangesAsync();
                return;
            }
            _context.Config.Add(config);
            await _context.SaveChangesAsync();
        }

        public async Task<Config> GetConfiguration()
        {
            var config = await _context.Config.FirstAsync();
            return config;
        }
    }
}
