using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class TriviaOption
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TriviaQuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public virtual TriviaQuestion TriviaQuestion { get; set; }
    }
}
