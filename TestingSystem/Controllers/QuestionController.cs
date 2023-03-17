using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.Models;

namespace TestingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public QuestionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetQuestion/{id}")]
        public async Task<ActionResult> GetQuestionById(int id)
        {
            var question = await _context.TriviaQuestions.Include(o => o.Options).FirstOrDefaultAsync(x => x.Id == id);
            if(question == null)
            {
                return NotFound();
            }
            return Ok(question); 
        }

        [HttpGet]
        [Route("GetAllQuestions")]
        public async Task<ActionResult> GetAllQuestions()
        {
            var questions = await _context.TriviaQuestions.Include(o => o.Options).ToListAsync();
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }


        [HttpPost]
        [Route("CreateQuestion")]
        public ActionResult CreateQuiz([FromBody] QuestionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quiz = _mapper.Map<TriviaQuestion>(model);

            _context.TriviaQuestions.Add(quiz);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut]
        [Route("UpdateQuestion")]
        public async Task<ActionResult> PutQuestion([FromBody] QuestionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var question = await _context.TriviaQuestions.Include(o => o.Options).FirstOrDefaultAsync(i => i.Id == model.Id);
            if (question == null)
            {
                return BadRequest();
            }


            question.Title = model.Title;
            question.Options = _mapper.Map<List<TriviaOption>>(model.Options);

            _context.TriviaQuestions.Update(question);
            await _context.SaveChangesAsync();

            return Ok(question);
        }

        [HttpDelete]
        [Route("DeleteQuestion/{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            var quiz = await _context.TriviaQuestions.FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            _context.TriviaQuestions.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
