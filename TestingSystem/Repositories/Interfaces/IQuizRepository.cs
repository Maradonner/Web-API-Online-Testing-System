using TestingSystem.Models;

namespace TestingSystem.Repositories.Interfaces;

public interface IQuizRepository
{
    Task<int> GetTotalPagesAsync(int pageSize);
    Task<TriviaQuiz> GetQuizByIdAsync(int id);
    Task<List<TriviaQuiz>> GetQuizPageAsync(int pageNumber, int pageSize);
    Task<List<TriviaQuiz>> GetAllQuizAsync();
    Task<ActiveTrivia> GetActiveTriviaAsync(int userId, int triviaQuizId);
    Task CreateQuizAsync(TriviaQuiz quiz);
    Task UpdateQuizAsync(TriviaQuiz quiz);
    Task<List<TriviaQuiz>> GetQuizzesByUserIdAsync(int userId);
    Task<bool> DeleteQuizAsync(TriviaQuiz quiz);
    Task<bool> DeleteQuizAsync(int id);
}