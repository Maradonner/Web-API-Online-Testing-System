using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.DTOs;
using TestingSystem.Models;
using TestingSystem.Repositories.Interfaces;

namespace TestingSystem.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserForDisplayDto> GetUserInfo(int userId)
    {
        var createdTests = await _context.TriviaQuizzes.CountAsync(x => x.UserId == userId);
        var startedQuizzes = await _context.ActiveTrivias.CountAsync(x => x.UserId == userId);
        var user = await _context.Users.FindAsync(userId);
        var userForDisplay = _mapper.Map<UserForDisplayDto>(user);

        userForDisplay.CreatedTests = createdTests;
        userForDisplay.StartedQuizzes = startedQuizzes;

        return userForDisplay;
    }

    //public async Task<object> GetUserInfo(int userId)
    //{
    //    return await _context.Users
    //        .Where(x => x.Id == userId)
    //        .Select(x => new
    //        {
    //            x.ImageUrl,
    //            role = x.Role.Name,
    //            x.Username
    //        }).FirstOrDefaultAsync();
    //}

    public async Task<List<ActiveTrivia>> GetUserAttempts(int userId)
    {
        return await _context.ActiveTrivias
            .Where(x => x.UserId == userId)
            .Include(x => x.Answers)
            .Include(x => x.TriviaQuiz)
            .OrderByDescending(x => x.StartTime)
            .ToListAsync();
    }
}