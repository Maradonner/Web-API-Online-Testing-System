using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingSystem.DTOs;
using TestingSystem.Models;
using TestingSystem.Services.CourseService;

namespace TestingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public CourseController(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var createdCourse = await _courseService.CreateCourseAsync(course);
            return CreatedAtAction("GetCourse", new { id = createdCourse.Id }, createdCourse);
        }

        // GET: api/courses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/courses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);

            if (id != course.Id)
            {
                return BadRequest();
            }

            try
            {
                await _courseService.UpdateCourseAsync(id, course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_courseService.CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("join/{courseCode}")]
        public async Task<ActionResult> JoinCourseByCode(int userId, string courseCode)
        {
            var user = await _courseService.JoinCourseByCodeAsync(userId, courseCode);

            if (user == null)
            {
                return NotFound("Course not found or user not found");
            }

            return Ok("Successfully joined the course");
        }

        // POST: api/courses/{courseId}/students/{studentId}
        [HttpPost("{courseId}/students/{studentId}")]
        public async Task<IActionResult> AddStudentToCourse(int courseId, int studentId)
        {
            var result = await _courseService.AddStudentToCourseAsync(courseId, studentId);

            if (!result)
            {
                return BadRequest("Failed to add the student to the course.");
            }

            return Ok();
        }


        // POST: api/courses/join/{courseCode}/students/{studentId}
        [HttpPost("join/{courseCode}/students/{studentId}")]
        public async Task<IActionResult> AddStudentToCourseByCode(string courseCode, int studentId)
        {
            var result = await _courseService.AddStudentToCourseByCodeAsync(courseCode, studentId);

            if (!result)
            {
                return BadRequest("Failed to add the student to the course.");
            }

            return Ok();
        }

        // DELETE: api/courses/{courseId}/students/{studentId}
        [HttpDelete("{courseId}/students/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromCourse(int courseId, int studentId)
        {
            var result = await _courseService.RemoveStudentFromCourseAsync(courseId, studentId);

            if (!result)
            {
                return BadRequest("Failed to remove the student from the course.");
            }

            return Ok();
        }
    }
}
