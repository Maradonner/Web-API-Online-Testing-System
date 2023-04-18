namespace TestingSystem.Models
{
    public class QuizDTO
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public List<QuestionDTO> Questions { get; set; }
        public int QuestionTime { get; set; }
        public int LivesCount { get; set; }
        public int AccumulateTime { get; set; }
        public string PictureUrl { get; set; }
    }
}
