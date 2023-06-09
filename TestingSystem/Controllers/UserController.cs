using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingSystem.Repositories.Interfaces;
using TestingSystem.Services.AuthService;
using TestingSystem.Services.QuizService;

namespace TestingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;
    private readonly IQuizService _quizService;
    public UserController(IUserRepository userRepository, IAuthService authService, IQuizService quizService)
    {
        _userRepository = userRepository;
        _authService = authService;
        _quizService = quizService;
    }

    [HttpGet("profile/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetInfo(int id)
    {
        var info = await _userRepository.GetUserInfo(id);
        return Ok(info);
    }

    [HttpGet("profile")]
    public async Task<ActionResult> GetMyInformation()
    {
        var info = await _userRepository.GetUserInfo(GetUserId());
        return Ok(info);
    }

    [HttpGet("GetAllAttempts")]
    public async Task<ActionResult> GetAllAttempts()
    {
        var attempts = await _userRepository.GetUserAttempts(GetUserId());
        var quizList = new List<object>();

        foreach (var activeTrivia in attempts)
        {
            (var isCompleted, var score) = await _quizService.CheckUserCompletionStatusAsync(GetUserId(), activeTrivia.TriviaQuizId);

            quizList.Add(new
            {
                activeTrivia.Id,
                activeTrivia.TriviaQuizId,
                activeTrivia.Answers.Count,
                activeTrivia.StartTime,
                IsCompleted = isCompleted,
                Score = score
            });
        }

        return Ok(quizList);
    }

    [HttpPut("Edit")]
    public async Task<ActionResult> EditAccount(string oldPassword, string newPassword, CancellationToken ct)
    {
        await _authService.ChangePasswordAsync(oldPassword, newPassword, GetUserId(), ct);
        return Ok();
    }

    private int GetUserId()
    {
        int.TryParse(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);
        return userId;
    }
}