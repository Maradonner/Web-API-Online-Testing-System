using IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using TestingSystem.Models;
using TestingSystem.Services.CourseService;

namespace IntegrationTests;

public class CourseTest : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GeneratorCodeTest()
    {
        const int length = 6;
        var code = CourseCodeGenerator.GenerateUniqueCode(_dbHelper.Context, length);
        Assert.That(code.Length, Is.EqualTo(length));
    }

    [Test]
    public async Task CreateTest()
    {
        using (var scope = Helper.CreateTransactionScope())
        {
            var course = new Course();
            Assert.Throws<DbUpdateException>(delegate
            {
                _courseService.CreateCourseAsync(course).GetAwaiter().GetResult();
            });

            course.TeacherId = 4;
            course.Name = "test";
            course.Description = "test";

            var createdCourse = await _courseService.CreateCourseAsync(course);

            Assert.NotNull(createdCourse);
            Assert.IsNotEmpty(createdCourse.CourseCode);
            Assert.AreEqual(course.Name, createdCourse.Name);
            Assert.AreEqual(course.Description, createdCourse.Description);
            Assert.AreEqual(course.TeacherId, createdCourse.TeacherId);
        }
    }

    [Test]
    public async Task DeleteTest()
    {
        using (var scope = Helper.CreateTransactionScope())
        {
            var course = new Course
            {
                TeacherId = 4,
                Name = "test",
                Description = "test"
            };

            var createdCourse = await _courseService.CreateCourseAsync(course);
            await _courseService.DeleteCourseAsync(createdCourse.Id);
        }
    }
}