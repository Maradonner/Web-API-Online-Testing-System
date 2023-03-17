using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestingSystem.Data;
using TestingSystem.Models;

namespace TestingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
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


    }
}
