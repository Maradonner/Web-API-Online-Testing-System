using Microsoft.EntityFrameworkCore;
using TestingSystem.Data;
using TestingSystem.Models;

namespace TestingSystem.Services.CourseService
{
    public static class CourseCodeGenerator
    {
        public static string GenerateUniqueCode(AppDbContext context, int length = 6)
        {
            string code;
            do
            {
                code = GenerateRandomCode(length);
            } while (context.Courses.Any(c => c.CourseCode == code));

            return code;
        }

        private static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            course.CourseCode = CourseCodeGenerator.GenerateUniqueCode(_context);
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<User> JoinCourseByCodeAsync(int userId, string courseCode)
        {
            var course = _context.Courses.Include(c => c.StudentCourses).FirstOrDefault(c => c.CourseCode == courseCode);
            if (course == null)
            {
                return null;
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }

            //course.Students.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateCourseAsync(int id, Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddStudentToCourseAsync(int courseId, int studentId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            var student = await _context.Users.FindAsync(studentId);

            if (course == null || student == null)
            {
                return false;
            }

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.StudentCourses.Add(studentCourse);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveStudentFromCourseAsync(int courseId, int studentId)
        {
            var studentCourse = await _context.StudentCourses
                .Where(sc => sc.StudentId == studentId && sc.CourseId == courseId)
                .FirstOrDefaultAsync();

            if (studentCourse == null)
            {
                return false;
            }

            _context.StudentCourses.Remove(studentCourse);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddStudentToCourseByCodeAsync(string courseCode, int studentId)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseCode == courseCode);
            var student = await _context.Users.FindAsync(studentId);

            if (course == null || student == null)
            {
                return false;
            }

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = course.Id
            };

            _context.StudentCourses.Add(studentCourse);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
