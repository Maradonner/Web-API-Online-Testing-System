namespace TestingSystem.Models;

public class TriviaOption
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCorrect { get; set; }
    public virtual TriviaQuestion TriviaQuestion { get; set; }
    public int TriviaQuestionId { get; set; }
}