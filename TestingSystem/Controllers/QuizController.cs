using AutoMapper;
using Keycloak.Net.Models.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using TestingSystem.Data;
using TestingSystem.Hubs;
using TestingSystem.Models;

namespace TestingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 10;
        private readonly IHubContext<QuizHub> _quizHub;


        public QuizController(AppDbContext context, IMapper mapper, IHubContext<QuizHub> quizHub)
        {
            _context = context;
            _mapper = mapper;
            _quizHub = quizHub;
        }

        [HttpGet("TotalPages")]
        public async Task<ActionResult> GetPageCount()
        {
            var totalPages = (int)Math.Ceiling(await _context.TriviaQuizs.CountAsync() / (double)_pageSize);

            return Ok(totalPages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuizById(int id)
        {
            var question = await _context.TriviaQuizs
                .Include(o => o.Questions)
                .ThenInclude(o => o.Options)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpGet("Page/{pageNumber}")]
        public async Task<ActionResult> GetQuizPage(int pageNumber)
        {
            var totalPages = (int)Math.Ceiling(await _context.TriviaQuizs.CountAsync() / (double)_pageSize);

            if(pageNumber < 1 || pageNumber > totalPages)
            {
                return BadRequest("Page is not found");
            }

            var questions = await _context.TriviaQuizs
                .Skip((pageNumber - 1) * _pageSize)
                .Take(_pageSize)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.UserId,
                    x.AccumulateTime,
                    x.LivesCount,
                    x.PictureUrl,
                    x.QuestionTime,
                }).ToListAsync();


            return Ok(questions);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllQuiz()
        {
            var questions = await _context.TriviaQuizs
                .Include(o => o.Questions)
                .ThenInclude(o => o.Options)
                .ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        [HttpGet("DisplayAll")]
        public async Task<ActionResult> DisplayAllQuiz()
        {
            var questions = await _context.TriviaQuizs
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.UserId,
                    x.AccumulateTime,
                    x.LivesCount,
                    x.PictureUrl,
                    x.QuestionTime,
                })
                .ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        [HttpPost("create-quiz")]
        public async Task<ActionResult> CreateQuiz([FromBody] QuizDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId = Convert.ToInt32(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            var quiz = _mapper.Map<TriviaQuiz>(model);

            quiz.UserId = userId;


            await _context.TriviaQuizs.AddAsync(quiz);
            await _context.SaveChangesAsync();

            var userName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            await _quizHub.Clients.All.SendAsync("QuizCreated", quiz.Id, quiz.Title, userName);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutQuestion([FromBody] QuizDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var quiz = await _context.TriviaQuizs
                .Include(q => q.Questions)
                .ThenInclude(o => o.Options)
                .FirstOrDefaultAsync(i => i.Id == model.Id);

            if (quiz == null)
            {
                return BadRequest();
            }

            quiz.AccumulateTime = model.AccumulateTime;
            quiz.LivesCount = model.LivesCount;
            quiz.PictureUrl = model.PictureUrl;
            quiz.Questions = _mapper.Map<List<TriviaQuestion>>(model.Questions);
            quiz.QuestionTime = model.QuestionTime;
            quiz.Title = model.Title;


            _context.TriviaQuizs.Update(quiz);
            await _context.SaveChangesAsync();

            return Ok(quiz);
        }

        [Authorize]
        [HttpGet("GetMyQuizzes")]
        public async Task<ActionResult> GetMyQuizzes()
        {
            int userId = Convert.ToInt32(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var quizzes = await _context.TriviaQuizs.Where(x => x.UserId == userId).ToListAsync();

            return Ok(quizzes);
        }



    }
}
