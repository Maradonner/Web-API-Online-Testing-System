using TestingSystem.Services.CourseService;

namespace IntegrationTests.Helpers;

public class BaseTest
{
    protected ICourseService _courseService;
    protected DbContextHelper _dbHelper = new();

    public BaseTest()
    {
        _courseService = new CourseService(_dbHelper.Context);
    }
}