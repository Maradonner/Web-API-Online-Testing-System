using TestingSystem.Models;

namespace TestingSystem.Repositories.Interfaces;

public interface IQuestionRepository
{
    Task<TriviaQuestion> GetQuestionByIdAsync(int id);
    Task<List<TriviaQuestion>> GetAllQuestionsAsync();
    Task CreateQuestionAsync(TriviaQuestion question);
    void UpdateQuestion(TriviaQuestion question);
    Task DeleteQuestionAsync(int id);
}