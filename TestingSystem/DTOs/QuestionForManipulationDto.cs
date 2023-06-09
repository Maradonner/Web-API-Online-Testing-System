using TestingSystem.Models;

namespace TestingSystem.DTOs;

public abstract class QuestionForManipulationDto
{
    public string Title { get; set; }
    public string PictureUrl { get; set; }
    public List<OptionForDisplayDto> Options { get; set; }
}