namespace TestingSystem.Models;

public class QuestionDto
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string PictureUrl { get; set; }
    public List<OptionDto> Options { get; set; }
}