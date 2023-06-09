namespace TestingSystem.DTOs;

public class QuizForDisplayDto : QuizForManipulationDto
{
    public bool IsCompleted { get; set; }
    public int Score { get; set; }
}