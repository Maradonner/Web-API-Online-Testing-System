using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.Models;
using TestingSystem.Repositories.Interfaces;
using TestingSystem.Services.AuthService;

namespace TestingSystem.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context, IMapper mapper, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<object> GetUserInfo(int userId)
    {
        return await _context.Users
            .Where(x => x.Id == userId)
            .Select(x => new
            {
                x.ImageUrl,
                role = x.Role.Name,
                x.Username
            }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ActiveTrivia>> GetUserAttempts(int userId)
    {
        return await _context.ActiveTrivias
            .Where(x => x.UserId == userId)
            .Include(x => x.Answers)
            .Include(x => x.TriviaQuiz)
            .OrderByDescending(x => x.StartTime)
            .ToListAsync();
    }
}