namespace TestingSystem.DTOs;

public class AnswerResponse
{
    public string CorrectAnswer { get; set; }
    public bool IsCorrect { get; set; }
    public bool IsTimedOut { get; set; }
    public bool IsFinished { get; set; }
    public int LivesCount { get; set; }
    public int LivesLeft { get; set; }
    public StateData Question { get; set; }
}