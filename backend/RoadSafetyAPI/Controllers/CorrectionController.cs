using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadSafetyAPI.DTOs.Correction;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Controllers;

[ApiController]
[Route("api/corrections")]
[Authorize]
public class CorrectionController : ControllerBase
{
    private readonly ICorrectionService _correctionService;

    public CorrectionController(ICorrectionService correctionService)
    {
        _correctionService = correctionService;
    }

    [HttpGet("question/{questionId}")]
    public async Task<IActionResult> GetByQuestion(int questionId)
    {
        var correction = await _correctionService.GetByQuestionIdAsync(questionId);
        if (correction == null) return NotFound();
        return Ok(correction);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateCorrectionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var correction = await _correctionService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByQuestion), new { questionId = correction.QuestionId }, correction);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCorrectionDto dto)
    {
        var correction = await _correctionService.UpdateAsync(id, dto);
        if (correction == null) return NotFound();
        return Ok(correction);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _correctionService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
