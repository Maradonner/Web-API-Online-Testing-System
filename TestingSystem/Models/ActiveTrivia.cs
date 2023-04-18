namespace TestingSystem.Models;

public class ActiveTrivia
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public List<Answer> Answers { get; set; }
    public int TriviaQuizId { get; set; }
    public virtual TriviaQuiz TriviaQuiz { get; set; }
    public int? UserId { get; set; }
}