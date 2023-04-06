using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestingSystem.Data;
using TestingSystem.Models;
using TestingSystem.Services.AuthService;

namespace TestingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserController(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpGet("GetInfo")]
        public async Task<ActionResult> GetInfo()
        {
            var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest();
            }

            int userIdInt = int.Parse(userId);
            var info = await _context.Users.Where(x => x.Id == userIdInt).Select(x => new
            {
                x.ImageUrl,
                role = x.Role.Name,
                x.Username,
                //x.TriviaQuiz
            }).FirstOrDefaultAsync();

            return Ok(info);
        }

        [HttpGet("GetAllAttempts")]
        public async Task<ActionResult> GetAllAttempts()
        {
            var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest();
            }

            int userIdInt = int.Parse(userId);

            var attempts = await _context.ActiveTrivias.Where(x => x.UserId == userIdInt).Include(x => x.Answers).Include(x => x.TriviaQuiz).ToListAsync();
            return Ok(attempts);
        }

        [AllowAnonymous]
        [HttpPut("Edit")]
        public async Task<ActionResult> EditAccount(string oldPassword, string newPassword, int userId)
        {
            var result = await _authService.ChangePassword(oldPassword, newPassword, userId);
            return Ok(result);
        }


    }
}
