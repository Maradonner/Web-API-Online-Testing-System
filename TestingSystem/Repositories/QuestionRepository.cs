using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.Models;
using TestingSystem.Repositories.Interfaces;

namespace TestingSystem.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TriviaQuestion> GetQuestionByIdAsync(int id)
    {
        return await _context.TriviaQuestions
            .Include(o => o.Options)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TriviaQuestion>> GetAllQuestionsAsync()
    {
        return await _context.TriviaQuestions
            .Include(o => o.Options)
            .ToListAsync();
    }

    public async Task CreateQuestionAsync(TriviaQuestion question)
    {
        await _context.TriviaQuestions.AddAsync(question);
        await _context.SaveChangesAsync();
    }

    public void UpdateQuestion(TriviaQuestion question)
    {
        _context.TriviaQuestions.Update(question);
        _context.SaveChanges();
    }

    public async Task DeleteQuestionAsync(int id)
    {
        var quiz = await _context.TriviaQuestions.FirstOrDefaultAsync(q => q.Id == id);
        _context.TriviaQuestions.Remove(quiz);
        await _context.SaveChangesAsync();
    }
}