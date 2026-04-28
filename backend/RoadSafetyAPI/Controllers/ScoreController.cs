using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadSafetyAPI.DTOs.Score;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Controllers;

[ApiController]
[Route("api/scores")]
[Authorize]
public class ScoreController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public ScoreController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpGet("student/{studentProfileId}")]
    public async Task<IActionResult> GetByStudent(int studentProfileId)
    {
        var scores = await _scoreService.GetByStudentIdAsync(studentProfileId);
        return Ok(scores);
    }

    [HttpPost]
    public async Task<IActionResult> Record([FromBody] CreateScoreDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var score = await _scoreService.RecordAsync(dto);
        return Ok(score);
    }
}
