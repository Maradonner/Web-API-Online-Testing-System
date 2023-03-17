﻿namespace TestingSystem.Models
{
    public class TriviaQuiz
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public int QuestionTime { get; set; }
        public int? LivesCount { get; set; }
        public int? AccumulateTime { get; set; }
        public string PictureUrl { get; set; } = String.Empty;
        public List<TriviaQuestion> Questions { get; set; }
    }
}
