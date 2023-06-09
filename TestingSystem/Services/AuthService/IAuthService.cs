using System.Data;
using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Services.AuthService;

public interface IAuthService
{
    Task<User> RegisterUserAsync(UserDto request, CancellationToken ct);
    Task<AuthResponseDto> LoginAsync(UserDto request, CancellationToken ct);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken, CancellationToken ct);
    Task<AuthResponseDto> ChangePasswordAsync(string oldPassword, string newPassword, int userId, CancellationToken ct);
    Task<User?> GetUserAsync(string email, CancellationToken ct);
    IDbTransaction BeginTransaction();
}