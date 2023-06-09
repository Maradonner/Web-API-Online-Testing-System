using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.Models;
using TestingSystem.Repositories.Interfaces;

namespace TestingSystem.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalPagesAsync(int pageSize)
    {
        return (int)Math.Ceiling(await _context.TriviaQuizzes.CountAsync() / (double)pageSize);
    }

    public async Task<TriviaQuiz> GetQuizByIdAsync(int id)
    {
        return await _context.TriviaQuizzes
            .Include(o => o.Questions)
                .ThenInclude(o => o.Options)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TriviaQuiz>> GetQuizPageAsync(int pageNumber, int pageSize)
    {
        return await _context.TriviaQuizzes
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<TriviaQuiz>> GetAllQuizAsync()
    {
        return await _context.TriviaQuizzes
            .Include(o => o.Questions)
            .ThenInclude(o => o.Options)
            .ToListAsync();
    }

    public async Task<ActiveTrivia> GetActiveTriviaAsync(int userId, int triviaQuizId)
    {
        return await _context.ActiveTrivias
            .Include(at => at.Answers)
            .FirstOrDefaultAsync(at => at.UserId == userId && at.TriviaQuizId == triviaQuizId);
    }

    public async Task CreateQuizAsync(TriviaQuiz quiz)
    {
        await _context.TriviaQuizzes.AddAsync(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQuizAsync(TriviaQuiz quiz)
    {
        _context.TriviaQuizzes.Update(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TriviaQuiz>> GetQuizzesByUserIdAsync(int userId)
    {
        return await _context.TriviaQuizzes
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> DeleteQuizAsync(TriviaQuiz quiz)
    {
        _context.TriviaQuizzes.Remove(quiz);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteQuizAsync(int id)
    {
        var quiz = await _context.TriviaQuizzes.FindAsync(id);

        if (quiz == null)
            return false;

        _context.TriviaQuizzes.Remove(quiz);
        await _context.SaveChangesAsync();

        return true;
    }
}