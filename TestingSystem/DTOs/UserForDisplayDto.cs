namespace TestingSystem.DTOs;

public class UserForDisplayDto : UserForManipulationDto
{
    public int CreatedTests { get; set; }
    public int StartedQuizzes { get; set;}
}
