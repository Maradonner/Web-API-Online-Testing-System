using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Services.GameService;

public static class ShuffleClass
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
    {
        var elements = source.ToArray();

        for (var i = elements.Length - 1; i >= 0; i--)
        {
            // Swap element "i" with a random earlier element it (or itself)
            // ... except we don't really need to swap it fully, as we can
            // return it immediately, and afterwards it's irrelevant.
            var swapIndex = rng.Next(i + 1);
            yield return elements[swapIndex];
            elements[swapIndex] = elements[i];
        }
    }
}

public class GameService : IGameService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GameService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<StateData> StartQuiz(int id, int userId)
    {
        var quiz = await _context.TriviaQuizzes
            .Include(x => x.Questions)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (quiz == null) return null;

        var rnd = new Random();
        var questions = quiz.Questions.Shuffle(rnd).ToList();


        var activeTrivia = new ActiveTrivia
        {
            StartTime = DateTime.Now,
            TriviaQuiz = quiz,
            UserId = userId,
            Answers = questions.Select(q => new Answer
            {
                TriviaQuestionId = q.Id,
                ActiveTriviaId = id
            }).ToList()
        };

        await _context.ActiveTrivias.AddAsync(activeTrivia);
        await _context.SaveChangesAsync();

        var firstQuestion = questions.FirstOrDefault();
        var questionDetails = await GetQuestionDetails(firstQuestion.Id);

        return await CreateStateData(activeTrivia, questionDetails, 0);
    }

    public async Task<object> PostAnswerQuiz(AnswerDto answer)
    {
        bool isCorrectFlag = false, isFinished = false, isTimedOut = false;

        if (answer.ActiveTriviaId == 0 || answer.TriviaQuestionId == 0) return new { Message = "Invalid request data" };

        var activeTrivia = await GetActiveSessionById(answer.ActiveTriviaId);

        var currentAnswer = activeTrivia.Answers.FirstOrDefault(x => x.TriviaQuestionId == answer.TriviaQuestionId);
        if (currentAnswer == null) return new { message = "No Answer" };

        var nextAnswer = activeTrivia.Answers.FirstOrDefault(x => x.Id > currentAnswer.Id);
        if (nextAnswer == null) isFinished = true;
        //return new { Message = "Session has ended" };

        var options = await _context.TriviaOptions
            .Where(x => x.TriviaQuestionId == answer.TriviaQuestionId)
            .ToListAsync();

        var opt = options.FirstOrDefault(x => x.Id == answer.TriviaOptionId);
        var correctOption = options.FirstOrDefault(x => x.IsCorrect);

        await UpdateAnswerResult(currentAnswer, opt, correctOption);

        if (opt != null && opt.IsCorrect) isCorrectFlag = true;


        var question = nextAnswer != null ? await GetQuestionDetails(nextAnswer.TriviaQuestionId) : null;
        var mistakes = activeTrivia.Answers.Count(x => x.IsCorrect == false && x.CorrectAnswerId != 0);
        var liveCount = activeTrivia.TriviaQuiz.LivesCount ?? 0;


        var stateData = await CreateStateData(activeTrivia, question, mistakes);
        var dataToFront = await CreateAnswerResult(isCorrectFlag, isTimedOut, stateData, correctOption.Title, liveCount,
            mistakes, isFinished);

        return dataToFront;
    }

    private async Task<ActiveTrivia> GetActiveSessionById(int id)
    {
        var activeSession = await _context.ActiveTrivias
            .Include(x => x.TriviaQuiz)
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == id);


        if (activeSession == null) throw new ArgumentException($"Active session with id {id} was not found.");

        return activeSession;
    }

    private async Task<object> GetQuestionDetails(int TriviaQuestionId)
    {
        var question = await _context.TriviaQuestions
            .Where(x => x.Id == TriviaQuestionId)
            .Include(x => x.Options)
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.PictureUrl,
                Options = x.Options.Select(o => new
                {
                    o.Id,
                    o.Title,
                    o.TriviaQuestionId
                })
            })
            .FirstOrDefaultAsync();

        //if (question == null) throw new ArgumentException("Question was not found.");

        return question;
    }

    private async Task<StateData> CreateStateData(ActiveTrivia activeTrivia, object question, int mistakes)
    {
        return new StateData
        {
            AccumulateTime = 0,
            ActiveId = activeTrivia.Id,
            //LivesCount = 3,
            //LivesLeft = 3 - mistakes,
            Points = 0,
            Question = question,
            QuestionTime = 30
        };
    }

    private async Task UpdateAnswerResult(Answer ans, TriviaOption myOption, TriviaOption correctOption)
    {
        if (ans == null)
            throw new ArgumentException("Not valid");

        if (myOption == null)
        {
            ans.IsCorrect = false;
            ans.CorrectAnswerId = correctOption.Id;
            ans.TriviaOptionId = 0;
            _context.Answers.Update(ans);
            await _context.SaveChangesAsync();
            return;
        }

        var option = await _context.TriviaOptions.FirstOrDefaultAsync(x => x.Id == myOption.Id);
        ans.IsCorrect = option.IsCorrect;
        ans.CorrectAnswerId = correctOption.Id;
        ans.TriviaOptionId = option.Id;
        _context.Answers.Update(ans);
        await _context.SaveChangesAsync();
    }

    private async Task<AnswerResponse> CreateAnswerResult(bool isCorrect, bool isTimedOut, StateData question,
        string correctOption, int livesCount, int mistakes, bool isFinished)
    {
        if (mistakes > livesCount) isFinished = true;
        var response = new AnswerResponse
        {
            IsCorrect = isCorrect,
            IsTimedOut = isTimedOut,
            IsFinished = isFinished,
            CorrectAnswer = correctOption,
            Question = question,
            LivesCount = livesCount,
            LivesLeft = livesCount - mistakes
        };
        return response;
    }
}