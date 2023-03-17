namespace TestingSystem.Models
{
    public class TriviaOptionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? TriviaQuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
