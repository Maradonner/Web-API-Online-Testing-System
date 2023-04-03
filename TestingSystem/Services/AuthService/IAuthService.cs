using TestingSystem.DTOs;
using TestingSystem.Models;

namespace TestingSystem.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> RegisterUser(UserDto request);
        Task<AuthResponseDto> Login(UserDto request);
        Task<AuthResponseDto> RefreshToken(string refreshToken);
        Task<AuthResponseDto> ChangePassword(string oldPassword, string newPassword, int userId);
        //Task<bool> RestorePassword(string email);

    }
}
