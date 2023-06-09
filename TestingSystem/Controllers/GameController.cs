using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TestingSystem.DTOs;
using TestingSystem.Models;
using TestingSystem.Services.GameService;

namespace TestingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("StartTrivia/{id}")]
    public async Task<ActionResult> SetActiveAnswer(int id)
    {
        var userString = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int.TryParse(userString, out var userId);

        var ans = await _gameService.StartQuiz(id, userId);

        if (ans == null)
            return BadRequest();

        return Ok(ans);
    }

    [HttpPost("ContinueTrivia")]
    public async Task<ActionResult> PostAnswer([FromBody] AnswerDto answer)
    {
        var temp = await _gameService.PostAnswerQuiz(answer);

        switch (temp)
        {
            case "Victory":
                break;
            case "Losing":
                break;
            case "Error":
                return BadRequest();
        }

        if (temp == null)
            return BadRequest();

        if (temp is not AnswerResponse tempo)
            return BadRequest(temp);

        return Ok(tempo);
    }
}