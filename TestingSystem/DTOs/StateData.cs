using TestingSystem.Models;

namespace TestingSystem.DTOs
{
    public class StateData
    {
        public int ActiveId { get; set; }
        public int AccumulateTime { get; set; }
        public int LivesCount { get; set; }
        public int LivesLeft { get; set; }
        public int Points { get; set; }
        public object Question { get; set; }
        public int QuestionTime { get; set; }
    }
}
