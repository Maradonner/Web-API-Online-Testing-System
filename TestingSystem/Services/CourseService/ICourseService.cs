using TestingSystem.Models;

namespace TestingSystem.Services.CourseService;

public interface ICourseService
{
    Task<Course> CreateCourseAsync(Course course);
    Task<Course> GetCourseAsync(int id);
    Task UpdateCourseAsync(int id, Course course);
    Task DeleteCourseAsync(int id);
    Task<User> JoinCourseByCodeAsync(int userId, string courseCode);
    Task<bool> AddStudentToCourseByCodeAsync(string courseCode, int studentId);
    Task<bool> AddStudentToCourseAsync(int courseId, int studentId);
    Task<bool> RemoveStudentFromCourseAsync(int courseId, int studentId);
}