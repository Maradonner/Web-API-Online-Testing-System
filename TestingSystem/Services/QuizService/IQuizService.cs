using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Services.QuizService;

public interface IQuizService
{
    Task<int> GetTotalPagesAsync(int pageSize);
    Task<TriviaQuiz> GetQuizByIdAsync(int id);
    Task<List<TriviaQuiz>> GetQuizPageAsync(int pageNumber, int pageSize);
    Task<List<TriviaQuiz>> GetAllQuizAsync();
    Task<(bool isCompleted, int score)> CheckUserCompletionStatusAsync(int userId, int triviaQuizId);
    Task<TriviaQuiz> CreateQuizAsync(QuizForCreationDto model, int userId);
    Task UpdateQuizAsync(QuizForUpdateDto model);
    Task<List<TriviaQuiz>> GetMyQuizzesAsync(int userId);
    Task<bool> DeleteQuizAsync(int id);
    Task<bool> DeleteQuizAsync(TriviaQuiz quiz);
}