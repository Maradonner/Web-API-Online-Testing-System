namespace Client.Models
{
    public class TriviaQuestionDTO
    {
        public string Title { get; set; }
        public virtual List<TriviaOptionDTO> Options { get; set; }
    }
}
