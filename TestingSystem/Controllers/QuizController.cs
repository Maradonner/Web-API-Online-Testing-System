using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TestingSystem.Hubs;
using TestingSystem.Models;
using TestingSystem.Services.QuizService;

namespace TestingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly int _pageSize = 10;
    private readonly IHubContext<QuizHub> _quizHub;
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService, IHubContext<QuizHub> quizHub)
    {
        _quizService = quizService;
        _quizHub = quizHub;
    }

    [HttpGet("TotalPages")]
    public async Task<ActionResult> GetPageCount()
    {
        var totalPages = await _quizService.GetTotalPagesAsync(_pageSize);

        return Ok(totalPages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetQuizById(int id)
    {
        var question = await _quizService.GetQuizByIdAsync(id);

        if (question == null) return NotFound();
        return Ok(question);
    }

    [HttpGet("Page/{pageNumber}")]
    public async Task<ActionResult> GetQuizPage(int pageNumber)
    {
        var totalPages = await _quizService.GetTotalPagesAsync(_pageSize);

        if (pageNumber < 1 || pageNumber > totalPages) return BadRequest("Page is not found");

        var questions = await _quizService.GetQuizPageAsync(pageNumber, _pageSize);
        return Ok(questions);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllQuiz()
    {
        var questions = await _quizService.GetAllQuizAsync();

        return Ok(questions);
    }

    [HttpGet("DisplayAll")]
    public async Task<ActionResult> DisplayAllQuiz()
    {
        var currentUserId = GetUserId();
        var quizzes = await _quizService.GetAllQuizAsync();


        var quizList = new List<object>();

        foreach (var quiz in quizzes)
        {
            (var isCompleted, var score) = await _quizService.CheckUserCompletionStatusAsync(currentUserId, quiz.Id);

            quizList.Add(new
            {
                quiz.Id,
                quiz.Title,
                quiz.UserId,
                quiz.AccumulateTime,
                quiz.LivesCount,
                quiz.PictureUrl,
                quiz.QuestionTime,
                IsCompleted = isCompleted,
                Score = score
            });
        }

        return Ok(quizList);
    }

    [Authorize]
    [HttpPost("create-quiz")]
    public async Task<ActionResult> CreateQuiz([FromBody] QuizDto model)
    {
        var userId = GetUserId();
        var quiz = await _quizService.CreateQuizAsync(model, userId);
        await _quizHub.Clients.All.SendAsync("QuizCreated", model.Id, model.Title, userId);

        return Ok(quiz);
    }

    [HttpPut]
    public async Task<ActionResult> PutQuestion([FromBody] QuizDto model)
    {
        await _quizService.UpdateQuizAsync(model);
        return Ok(model);
    }

    [Authorize]
    [HttpGet("GetMyQuizzes")]
    public async Task<ActionResult> GetMyQuizzes()
    {
        var userId = Convert.ToInt32(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var quizzes = await _quizService.GetMyQuizzesAsync(userId);

        return Ok(quizzes);
    }

    private int GetUserId()
    {
        int.TryParse(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);
        return userId;
    }
}