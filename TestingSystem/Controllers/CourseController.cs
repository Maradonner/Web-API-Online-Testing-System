using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingSystem.DTOs;
using TestingSystem.Models;
using TestingSystem.Services.CourseService;

namespace TestingSystem.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly IMapper _mapper;

    public CourseController(IMapper mapper, ICourseService courseService)
    {
        _mapper = mapper;
        _courseService = courseService;
    }

    // POST: api/courses
    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(CourseDto courseDto)
    {
        var course = _mapper.Map<Course>(courseDto);
        course.TeacherId = GetUserId();

        var createdCourse = await _courseService.CreateCourseAsync(course);
        return CreatedAtAction("GetCourse", new { id = createdCourse.Id }, createdCourse);
    }

    // GET: api/courses/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        var course = await _courseService.GetCourseAsync(id);

        if (course == null) return NotFound();

        return course;
    }

    // PUT: api/courses/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, CourseDto courseDto)
    {
        var course = _mapper.Map<Course>(courseDto);

        await _courseService.UpdateCourseAsync(id, course);

        return NoContent();
    }

    [HttpPost("join/{courseCode}")]
    public async Task<ActionResult> JoinCourseByCode(int userId, string courseCode)
    {
        var user = await _courseService.JoinCourseByCodeAsync(userId, courseCode);

        if (user == null) return NotFound("Course not found or user not found");

        return Ok("Successfully joined the course");
    }

    [HttpPost("{courseId}/students/{studentId}")]
    public async Task<ActionResult> AddStudentToCourse(int courseId, int studentId)
    {
        var result = await _courseService.AddStudentToCourseAsync(courseId, studentId);

        if (!result) return BadRequest("Failed to add the student to the course.");

        return Ok();
    }


    [HttpPost("join/{courseCode}/students/{studentId}")]
    public async Task<ActionResult> AddStudentToCourseByCode(string courseCode, int studentId)
    {
        var result = await _courseService.AddStudentToCourseByCodeAsync(courseCode, studentId);

        if (!result) return BadRequest("Failed to add the student to the course.");

        return Ok();
    }

    [HttpDelete("{courseId}/students/{studentId}")]
    public async Task<ActionResult> RemoveStudentFromCourse(int courseId, int studentId)
    {
        var result = await _courseService.RemoveStudentFromCourseAsync(courseId, studentId);

        if (!result) return BadRequest("Failed to remove the student from the course.");

        return Ok();
    }

    private int GetUserId()
    {
        int.TryParse(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);
        return userId;
    }
}