using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserForDisplayDto> GetUserInfo(int userId);
    Task<List<ActiveTrivia>> GetUserAttempts(int userId);
}