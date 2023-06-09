using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.Models;

public class Answer
{
    public int Id { get; set; }
    [ForeignKey("ActiveTrivia")] 
    public int ActiveTriviaId { get; set; }
    [ForeignKey("TriviaOption")] 
    public int TriviaOptionId { get; set; }
    [ForeignKey("TriviaQuestion")] 
    public int TriviaQuestionId { get; set; }
    public bool IsCorrect { get; set; }
    [ForeignKey("TriviaOption")] 
    public int CorrectAnswerId { get; set; }
    public virtual ActiveTrivia ActiveTrivia { get; set; }
}