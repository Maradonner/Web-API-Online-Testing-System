namespace TestingSystem.DTOs;

public abstract class QuizForManipulationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = "";
    public List<QuestionForDisplayDto> Questions { get; set; } = new();
    public int QuestionTime { get; set; }
    public int LivesCount { get; set; }
    public bool AccumulateTime { get; set; }
    public string PictureUrl { get; set; } = "";
}