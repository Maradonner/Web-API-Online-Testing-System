using Microsoft.AspNetCore.Mvc;
using TestingSystem.Exceptions;
using TestingSystem.Models;
using TestingSystem.Services.AuthService;

namespace TestingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser(UserDto request, CancellationToken ct)
    {
        using var transaction = _authService.BeginTransaction();
        try
        {
            var response = await _authService.RegisterUserAsync(request, ct);
            transaction.Commit();
            return Ok(response);
        }
        catch (UsernameAlreadyExistsException)
        {
            transaction.Rollback();
            return Conflict("Username is already taken");
        }

    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(UserDto request, CancellationToken ct)
    {
        var response = await _authService.LoginAsync(request, ct);
        if (response.Success)
            return Ok(response);

        return BadRequest(response.Message);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<string>> RefreshToken(RefreshDto request, CancellationToken ct)
    {
        var response = await _authService.RefreshTokenAsync(request.RefreshToken, ct);
        if (response.Success)
            return Ok(response);

        return BadRequest(response.Message);
    }
}