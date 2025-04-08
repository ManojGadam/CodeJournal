using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalWebsite.Context;
using PersonalWebsite.Models;


namespace PersonalWebsite.Repository
{
        
    public interface IProblemRepository
    {
       Task<Boolean> SaveProblem(Problem problem, int userId);
        Task<List<Problem>> GetProblems(int userId);
        Task SaveConfiguration(Config config, int userId);
        Task<Config> GetConfiguration(int userId);
        Task<Problem> GetProblem(int id, int userId);
        Task<int> GetUserIdFromClaim(string userId);
        Task RegisterUser(User user);
    }
    public class ProblemRepository : IProblemRepository
    {
        private readonly ProblemContext _context;
        public ProblemRepository(ProblemContext context)
        {
            _context = context;
        }

        public async Task<Boolean> SaveProblem(Problem problem,int userId)
        {
            try
            {
                var currProblem = await _context.Problems.Where((x) => x.UserId == userId && (x.Id == problem.Id || x.Name == problem.Name)).FirstOrDefaultAsync();
                if (currProblem != null)
                {
                    currProblem.Comments = problem.Comments;
                    currProblem.Code = problem.Code;
                    currProblem.UserId = userId;
                    currProblem.ModifiedOn = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return true;
                }
                problem.UserId = userId;
                problem.CreatedOn = DateTime.UtcNow;
                problem.ModifiedOn = DateTime.UtcNow;
                _context.Problems.Add(problem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            }
        public async Task<List<Problem>> GetProblems(int userId)
        {
            try
            {
                var problems = await _context.Problems.Where(x => x.UserId == userId).ToListAsync();
                return problems;
            }
            catch (Exception) { 
                throw;
            }
        }
        public async Task SaveConfiguration(Config config, int userId)
        {
            try
            {
                var configAv = await _context.Config.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (configAv != null)
                {
                    configAv.GitURL = config.GitURL;
                    config.GitToken = configAv.GitToken;
                    await _context.SaveChangesAsync();
                    return;
                }
                _context.Config.Add(config);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Problem> GetProblem(int id, int userId)
        {
            try
            {
                var problem =  await _context.Problems.Where(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
                if (problem == null) throw new Exception("Id doesn't exist");
                return problem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Config> GetConfiguration(int userId)
        {
            try
            {
                var config = await _context.Config.Where(x => x.UserId == userId).FirstAsync();
                return config;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetUserIdFromClaim(string userId)
        {
            try
            {
              var id = await _context.User.FirstOrDefaultAsync(x => x.AuthId == userId);
              return id == null ? 0 : id.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task RegisterUser(User user)
        {
            try
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
