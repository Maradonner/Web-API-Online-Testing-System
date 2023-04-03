using Microsoft.AspNetCore.Mvc;
using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Services.GameService
{
    public interface IGameService
    {
        Task<StateData> StartQuiz(int id, int userId);
        Task<object> PostAnswerQuiz(AnswerDTO answer);
    }
}
