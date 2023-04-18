using TestingSystem.Models;

namespace TestingSystem.Services.QuizService;

public interface IQuizService
{
    Task<int> GetTotalPagesAsync(int pageSize);
    Task<TriviaQuiz> GetQuizByIdAsync(int id);
    Task<List<TriviaQuiz>> GetQuizPageAsync(int pageNumber, int pageSize);
    Task<List<TriviaQuiz>> GetAllQuizAsync();
    Task<(bool isCompleted, int score)> CheckUserCompletionStatusAsync(int userId, int triviaQuizId);
    Task<TriviaQuiz> CreateQuizAsync(QuizDto model, int userId);
    Task UpdateQuizAsync(QuizDto model);
    Task<List<TriviaQuiz>> GetMyQuizzesAsync(int userId);
}