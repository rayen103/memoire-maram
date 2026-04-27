using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadSafetyAPI.DTOs.Answer;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Controllers;

[ApiController]
[Route("api/answers")]
[Authorize]
public class AnswerController : ControllerBase
{
    private readonly IAnswerService _answerService;

    public AnswerController(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    [HttpGet("question/{questionId}")]
    public async Task<IActionResult> GetByQuestionId(int questionId)
    {
        var answers = await _answerService.GetByQuestionIdAsync(questionId);
        return Ok(answers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var answer = await _answerService.GetByIdAsync(id);
        if (answer == null) return NotFound();
        return Ok(answer);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateAnswerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var answer = await _answerService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = answer.Id }, answer);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAnswerDto dto)
    {
        var answer = await _answerService.UpdateAsync(id, dto);
        if (answer == null) return NotFound();
        return Ok(answer);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _answerService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
