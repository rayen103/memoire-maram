using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadSafetyAPI.DTOs.Student;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Controllers;

[ApiController]
[Route("api/students")]
[Authorize]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("profile/{studentProfileId}")]
    public async Task<IActionResult> GetProfile(int studentProfileId)
    {
        var profile = await _studentService.GetProfileAsync(studentProfileId);
        if (profile == null) return NotFound();
        return Ok(profile);
    }

    [HttpGet("profile/user/{userId}")]
    public async Task<IActionResult> GetProfileByUserId(int userId)
    {
        var profile = await _studentService.GetProfileByUserIdAsync(userId);
        if (profile == null) return NotFound();
        return Ok(profile);
    }

    [HttpGet("{studentProfileId}/badges")]
    public async Task<IActionResult> GetBadges(int studentProfileId)
    {
        var badges = await _studentService.GetBadgesAsync(studentProfileId);
        return Ok(badges);
    }

    [HttpGet("{studentProfileId}/answers")]
    public async Task<IActionResult> GetAnswers(int studentProfileId)
    {
        var answers = await _studentService.GetAnswersAsync(studentProfileId);
        return Ok(answers);
    }

    [HttpGet("{studentProfileId}/quiz-result/{quizId}")]
    public async Task<IActionResult> GetQuizResult(int studentProfileId, int quizId)
    {
        var result = await _studentService.GetQuizResultAsync(studentProfileId, quizId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("submit-answer")]
    public async Task<IActionResult> SubmitAnswer([FromBody] SubmitAnswerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _studentService.SubmitAnswerAsync(dto);
        if (result == null)
            return BadRequest(new { message = "Answer not found." });

        return Ok(new
        {
            message = result.IsCorrect ? "Correct answer! +10 points." : "Incorrect answer.",
            isCorrect = result.IsCorrect
        });
    }
}
