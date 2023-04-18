using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingSystem.Repositories.Interfaces;
using TestingSystem.Services.AuthService;

namespace TestingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    [HttpGet("GetInfo")]
    public async Task<ActionResult> GetInfo()
    {
        var info = await _userRepository.GetUserInfo(GetUserId());
        return Ok(info);
    }

    [HttpGet("GetAllAttempts")]
    public async Task<ActionResult> GetAllAttempts()
    {
        var attempts = await _userRepository.GetUserAttempts(GetUserId());
        return Ok(attempts);
    }

    [HttpPut("Edit")]
    public async Task<ActionResult> EditAccount(string oldPassword, string newPassword)
    {
        await _authService.ChangePassword(oldPassword, newPassword, GetUserId());
        return Ok();
    }

    private int GetUserId()
    {
        int.TryParse(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);
        return userId;
    }
}