using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CourseCode { get; set; }

    [ForeignKey(nameof(User))] public int TeacherId { get; set; }

    public User Teacher { get; set; }
    public virtual List<TriviaQuiz> TriviaQuizzes { get; set; } = new();
    public virtual List<StudentCourse> StudentCourses { get; set; } = new();
}