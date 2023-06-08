using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Services.AuthService;

public interface IAuthService
{
    Task<User> RegisterUserAsync(UserDto request);
    Task<AuthResponseDto> LoginAsync(UserDto request);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    Task<AuthResponseDto> ChangePasswordAsync(string oldPassword, string newPassword, int userId);
    Task<User> GetUserAsync(string email);
}