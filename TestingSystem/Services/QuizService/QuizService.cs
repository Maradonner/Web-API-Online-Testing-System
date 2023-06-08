using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Services.QuizService;

public class QuizService : IQuizService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public QuizService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        var model = await _context.TriviaQuizzes
            .Include(o => o.Questions)
                .ThenInclude(o => o.Options)
            .ToListAsync();

        return model;
    }

    public async Task<(bool isCompleted, int score)> CheckUserCompletionStatusAsync(int userId, int triviaQuizId)
    {
        var activeTrivia = await _context.ActiveTrivias
            .Include(at => at.Answers)
            .FirstOrDefaultAsync(at => at.UserId == userId && at.TriviaQuizId == triviaQuizId);

        if (activeTrivia != null)
        {
            var score = activeTrivia.Answers.Count(a => a.IsCorrect);
            return (true, score);
        }

        return (false, 0);
    }

    public async Task<TriviaQuiz> CreateQuizAsync(QuizForCreationDto model, int userId)
    {
        var quiz = _mapper.Map<TriviaQuiz>(model);
        quiz.UserId = userId;

        await _context.TriviaQuizzes.AddAsync(quiz);
        await _context.SaveChangesAsync();

        return quiz;
    }

    public async Task UpdateQuizAsync(QuizForUpdateDto model)
    {
        var quiz = await _context.TriviaQuizzes
            .Include(q => q.Questions)
                .ThenInclude(o => o.Options)
            .FirstOrDefaultAsync(i => i.Id == model.Id);

        if (quiz != null)
        {
            quiz.AccumulateTime = model.AccumulateTime;
            quiz.LivesCount = model.LivesCount;
            quiz.PictureUrl = model.PictureUrl;
            quiz.Questions = _mapper.Map<List<TriviaQuestion>>(model.Questions);
            quiz.QuestionTime = model.QuestionTime;
            quiz.Title = model.Title;

            _context.TriviaQuizzes.Update(quiz);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TriviaQuiz>> GetMyQuizzesAsync(int userId)
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

    public Task<bool> DeleteQuizAsync(int id)
    {
        throw new NotImplementedException();
    }
}