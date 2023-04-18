namespace TestingSystem.Models;

public class QuizDto
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public List<QuestionDto> Questions { get; set; }
    public int QuestionTime { get; set; }
    public int LivesCount { get; set; }
    public bool AccumulateTime { get; set; }
    public string PictureUrl { get; set; }
}