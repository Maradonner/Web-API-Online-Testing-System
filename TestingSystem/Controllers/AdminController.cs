using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.Models;
using TestingSystem.Services.AuthService;

namespace TestingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "User")]
public class AdminController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AdminController(AppDbContext context, IMapper mapper, IAuthService authService)
    {
        _context = context;
        _mapper = mapper;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser(UserDto request)
    {
        var response = await _authService.RegisterUser(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        var users = await _context.Users.Include(r => r.Role).Select(x => new
        {
            x.Id,
            x.ImageUrl,
            x.Role!.Name,
            x.Username,
            Quizzes = x.TriviaQuiz,
        }).ToListAsync();
        return Ok(users);
    }

    [HttpPut("update-user")]
    public async Task<ActionResult> UpdateUser(UserDto model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == model.Id);

        if (user == null)
        {
            return BadRequest();
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(q => q.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}