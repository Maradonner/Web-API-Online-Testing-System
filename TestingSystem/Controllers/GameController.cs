using Microsoft.AspNetCore.Mvc;
using TestingSystem.DTOs;
using TestingSystem.Models;
using TestingSystem.Services.GameService;

namespace TestingSystem.Controllers
{
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
            var ans = await _gameService.StartQuiz(id);

            if(ans == null)
            {
                return BadRequest();
            }

            return Ok(ans);
        }

        [HttpPost("ContinueTrivia")]
        public async Task<ActionResult> PostAnswer([FromBody] AnswerDTO answer)
        {
            var temp = await _gameService.PostAnswerQuiz(answer);

            switch(temp)
            {
                case "Victory":
                    break;
                case "Losing":
                    break;
                case "Error":
                    return BadRequest();
            }

            if(temp == null)
            {
                return BadRequest();
            }

            var tempo = temp as AnswerResponse;
            if(tempo == null)
            {
                return BadRequest(temp);
            }
            return Ok(tempo);
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetCard(int id)
        //{
        //   var sss = await _context.TriviaQuizs.Include(x => x.Questions).FirstOrDefaultAsync(x => x.Id == id);

        //    if(sss == null)
        //    {
        //        return BadRequest();
        //    }

        //    var first = sss.Questions?.FirstOrDefault().Id;
        //    var last  = sss.Questions?.LastOrDefault().Id;
        //    //qwe.Questions

        //    var index = rnd.Next(24, 28);
        //    var quiz = await _context.TriviaQuestions.Include(x => x.Options).FirstOrDefaultAsync(i => i.Id == index);
        //    if (quiz == null)
        //    {
        //        return NotFound();
        //    }

        //    StateData data = new StateData()
        //    {
        //        LivesLeft = 3,
        //        LivesCount = 3,
        //        AccumulateTime = 0,
        //        GameId = 0,
        //        Points = 0,
        //        QuestionTime = 60,
        //        TriviaQuestion = quiz,
        //    };
        //    return Ok(data);
        //}

        //[HttpPost]
        //public async Task<ActionResult> PostCard([FromBody]AnswerDTO model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    bool isCorrect = false;
        //    var question = await _context.TriviaQuestions.Include(o => o.Options)
        //                                                 .FirstOrDefaultAsync(x => x.Id == model.StateData.TriviaQuestion.Id);

        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    var corr = new TriviaOption();
        //    int points = model.StateData.Points;
        //    int lives = model.StateData.LivesLeft;

        //    foreach (var elem in question.Options)
        //    {
        //        if (elem.IsCorrect)
        //        {
        //            corr = elem;
        //        }
        //        if (elem.Id == model.Answer.Id && elem.IsCorrect)
        //        {
        //            isCorrect = true;
        //            points++;
        //            break;
        //        }
        //    }

        //    var correctAnswer = _mapper.Map<TriviaOptionDTO>(corr);

        //    var index = rnd.Next(8, 14);
        //    var quiz = await _context.TriviaQuestions.Include(x => x.Options).FirstOrDefaultAsync(i => i.Id == index);

        //    if (quiz == null)
        //    {
        //        return NotFound();
        //    }

        //    if (!isCorrect && model.StateData.LivesLeft < 1)
        //    {
        //        return BadRequest("You are loser");
        //    }
        //    if (!isCorrect && model.StateData.LivesLeft > 0)
        //    {
        //        lives--;
        //    }

        //    StateData nextQuestion = new StateData()
        //    {
        //        LivesLeft = lives,
        //        LivesCount = 3,
        //        AccumulateTime = 0,
        //        GameId = 0,
        //        Points = points,
        //        QuestionTime = 60,
        //        TriviaQuestion = quiz,
        //    };

        //    AnswerDTO answer = new AnswerDTO()
        //    {
        //        correctAnswer = correctAnswer.Title,
        //        isCorrect = isCorrect,
        //        isTimedOut = false,
        //        StateData = nextQuestion
        //    };
        //    return Ok(answer);
        //}
    }
}
