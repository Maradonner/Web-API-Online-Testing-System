namespace TestingSystem.DTOs;

public class PostAnswer
{
    public string Question { get; set; }
    public string isTimedOut { get; set; }
    public string isCorrect { get; set; }
    public string correctAnswer { get; set; }
    public string RefreshToken { get; set; }
}