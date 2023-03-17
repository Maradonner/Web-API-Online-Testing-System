using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.Models
{
    public class TriviaQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PictureUrl { get; set; } = String.Empty;
        public List<TriviaOption> Options { get; set; }
        public int TriviaQuizId { get; set; }
        public virtual TriviaQuiz TriviaQuiz { get; set; }
    }
}