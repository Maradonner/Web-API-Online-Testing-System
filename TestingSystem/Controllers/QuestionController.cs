using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.DTOs;
using TestingSystem.Models;
using TestingSystem.Repositories.Interfaces;

namespace TestingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public QuestionController(IQuestionRepository repository, IMapper mapper)
    {
        _questionRepository = repository;
        _mapper = mapper;
    }

    [HttpGet("GetQuestion/{id}", Name = "Question")]
    public async Task<ActionResult> GetQuestionById(int id)
    {
        var question = await _questionRepository.GetQuestionByIdAsync(id);

        if (question == null)
            return NotFound();

        return Ok(question);
    }

    [HttpGet("GetAllQuestions")]
    public async Task<ActionResult> GetAllQuestions()
    {
        var questions = await _questionRepository.GetAllQuestionsAsync();

        if (questions == null)
            return NotFound();

        return Ok(questions);
    }

    [HttpPost("CreateQuestion")]
    public async Task<ActionResult> CreateQuiz([FromBody] QuestionForCreationDto model)
    {
        var quiz = _mapper.Map<TriviaQuestion>(model);

        await _questionRepository.CreateQuestionAsync(quiz);

        return CreatedAtRoute("Question", new { id = quiz.Id }, quiz);
    }

    [HttpPut("UpdateQuestion")]
    public async Task<ActionResult> PutQuestion([FromBody] QuestionForUpdateDto model)
    {
        var question = await _questionRepository.GetQuestionByIdAsync(model.Id);

        if (question == null)
            return BadRequest();

        question.Title = model.Title;
        question.Options = _mapper.Map<List<TriviaOption>>(model.Options);

        _questionRepository.UpdateQuestion(question);

        return Ok(question);
    }

    [HttpDelete("DeleteQuestion/{id}")]
    public async Task<ActionResult> DeleteQuestion(int id)
    {
        await _questionRepository.DeleteQuestionAsync(id);

        return NoContent();
    }
}