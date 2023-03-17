namespace TestingSystem.Models
{
    public class QuestionDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string PictureUrl { get; set; }
        public List<OptionDTO> Options { get; set; }
    }
}
