using Microsoft.AspNetCore.Mvc;
using TestingSystem.DTOs;
using TestingSystem.Repositories.Interfaces;

namespace TestingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OptionController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;

    public OptionController(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    [HttpGet("{id}", Name = "Option")]
    public async Task<ActionResult> GetOptionById(int id)
    {
        return Ok();
    }
    
    [HttpPost("CreateOption")]
    public async Task<ActionResult> CreateQuiz([FromBody] OptionForCreationDto model)
    {
        return Ok();
        //return CreatedAtRoute("Option", new { id = quiz.Id }, quiz);
    }
}