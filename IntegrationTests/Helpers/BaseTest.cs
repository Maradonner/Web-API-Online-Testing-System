using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Services.CourseService;

namespace IntegrationTests.Helpers
{
    public class BaseTest
    {
        protected DbContextHelper _dbHelper = new();
        protected ICourseService _courseService;

        public BaseTest()
        {
            _courseService = new CourseService(_dbHelper.Context);
        }
    }
}
