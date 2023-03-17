using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class TriviaQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TriviaOption> Options { get; set; }
    }
}
