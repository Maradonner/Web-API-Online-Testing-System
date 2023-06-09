using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TestingSystem.DTOs;
using TestingSystem.Hubs;
using TestingSystem.Services.QuizService;

namespace TestingSystem.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly int _pageSize = 10;
    private readonly IHubContext<QuizHub> _quizHub;
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService, IHubContext<QuizHub> quizHub, IMapper mapper)
    {
        _quizService = quizService;
        _quizHub = quizHub;
        _mapper = mapper;
    }

    [HttpGet("TotalPages")]
    public async Task<ActionResult> GetPageCount()
    {
        var totalPages = await _quizService.GetTotalPagesAsync(_pageSize);

        return Ok(totalPages);
    }

    [HttpGet("{id}", Name = "GetQuiz")]
    public async Task<ActionResult> GetQuizById(int id)
    {
        var question = await _quizService.GetQuizByIdAsync(id);

        if (question == null)
            return NotFound();
        return Ok(question);
    }

    [HttpPost("create-quiz")]
    public async Task<ActionResult> CreateQuiz([FromBody] QuizForCreationDto model)
    {
        var userId = GetUserId();
        var quiz = await _quizService.CreateQuizAsync(model, userId);
        await _quizHub.Clients.All.SendAsync("QuizCreated", model.Id, model.Title, userId);

        return CreatedAtRoute("GetQuiz", new { id = quiz.Id }, quiz);
    }

    [HttpGet("Page/{pageNumber}")]
    public async Task<ActionResult> GetQuizPage(int pageNumber)
    {
        var totalPages = await _quizService.GetTotalPagesAsync(_pageSize);

        if (pageNumber < 1 || pageNumber > totalPages)
            return BadRequest("Page is not found");

        var questions = await _quizService.GetQuizPageAsync(pageNumber, _pageSize);
        return Ok(questions);
    }

    [AllowAnonymous]
    [HttpGet("display-all")]
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

    [HttpPut]
    public async Task<ActionResult> PutQuestion([FromBody] QuizForUpdateDto model)
    {
        await _quizService.UpdateQuizAsync(model);
        return Ok(model);
    }

    [HttpGet("get-my-quizzes")]
    public async Task<ActionResult> GetMyQuizzes()
    {
        var quizzes = await _quizService.GetMyQuizzesAsync(GetUserId());
        return Ok(quizzes);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteQuiz(int id)
    {
        var quiz = await _quizService.GetQuizByIdAsync(id);

        if (quiz == null)
            return NotFound("Quiz is not found");
        if (quiz.UserId != GetUserId())
            return BadRequest("Action prohibited");

        var isSucceed = await _quizService.DeleteQuizAsync(quiz);

        if (!isSucceed)
            return BadRequest($"Unable to delete quiz {id}");

        return Ok("Quiz Successfully deleted");
    }


    private int GetUserId()
    {
        int.TryParse(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);
        return userId;
    }
}