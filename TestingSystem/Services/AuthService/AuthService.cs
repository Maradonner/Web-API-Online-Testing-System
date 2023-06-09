using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using TestingSystem.Data;
using TestingSystem.DTOs;
using TestingSystem.Exceptions;
using TestingSystem.Models;

namespace TestingSystem.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User> RegisterUserAsync(UserDto request, CancellationToken ct)
    {
        if (await GetUserAsync(request.Username, ct) != null)
            throw new UsernameAlreadyExistsException();

        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync(ct);
        return user;
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, ct);
        
        if (user == null)
            return new AuthResponseDto
            {
                Message = "Invalid Refresh Token"
            };
        if (user.TokenExpired < DateTime.Now)
            return new AuthResponseDto
            {
                Message = "Token expired"
            };

        var token = CreateToken(user);
        var newRefreshToken = CreateRefreshToken();
        await SetRefreshTokenAsync(newRefreshToken, user);

        return new AuthResponseDto
        {
            Success = true,
            Token = token,
            RefreshToken = newRefreshToken.Token,
            TokenExpires = newRefreshToken.Expires
        };
    }

    public async Task<AuthResponseDto> LoginAsync(UserDto request, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(u => u.Username == request.Username, ct);

        if (user == null) return new AuthResponseDto { Message = "User not found." };
        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return new AuthResponseDto { Message = "Wrong Password." };
        var token = CreateToken(user);
        var refreshToken = CreateRefreshToken();
        await SetRefreshTokenAsync(refreshToken, user);
        return new AuthResponseDto
        {
            Success = true,
            Token = token,
            RefreshToken = refreshToken.Token,
            TokenExpires = refreshToken.Expires
        };
    }

    public async Task<AuthResponseDto> ChangePasswordAsync(string oldPassword, string newPassword, int userId, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(u => u.Id == userId, ct);

        if (user == null) return new AuthResponseDto { Message = "User not found." };
        if (!VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
            return new AuthResponseDto { Message = "Wrong Password." };

        CreatePasswordHash(newPassword, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _context.Users.Update(user);
        await _context.SaveChangesAsync(ct);

        return new AuthResponseDto { Message = "Succeed" };
    }

    public async Task<User?> GetUserAsync(string email, CancellationToken ct)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Username == email, ct);
    }

    public IDbTransaction BeginTransaction()
    {
        var transaction = _context.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jet = new JwtSecurityTokenHandler().WriteToken(token);
        return jet;
    }

    private RefreshToken CreateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };
        return refreshToken;
    }

    private async Task SetRefreshTokenAsync(RefreshToken refreshToken, User user)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };

        _httpContextAccessor?.HttpContext?.Response
            .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

        user.RefreshToken = refreshToken.Token;
        user.TokenCreated = refreshToken.Created;
        user.TokenExpired = refreshToken.Expires;

        await _context.SaveChangesAsync();
    }


}