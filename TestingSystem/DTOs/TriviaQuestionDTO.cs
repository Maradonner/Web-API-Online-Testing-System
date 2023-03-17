using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.Models
{
    public class TriviaQuestionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<TriviaOptionDTO> Options { get; set; }
        public string Answer { get; set; }

    }
}
