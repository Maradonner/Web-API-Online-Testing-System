using TestingSystem.Models;

namespace TestingSystem.DTOs;

public abstract class UserForManipulationDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
