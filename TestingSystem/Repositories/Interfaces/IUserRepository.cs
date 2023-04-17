using TestingSystem.Models;

namespace TestingSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<object> GetUserInfo(int userId);
        Task<IEnumerable<ActiveTrivia>> GetUserAttempts(int userId);
    }
}
