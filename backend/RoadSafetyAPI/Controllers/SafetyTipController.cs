using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadSafetyAPI.DTOs.SafetyTip;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Controllers;

[ApiController]
[Route("api/safety-tips")]
[Authorize]
public class SafetyTipController : ControllerBase
{
    private readonly ISafetyTipService _safetyTipService;

    public SafetyTipController(ISafetyTipService safetyTipService)
    {
        _safetyTipService = safetyTipService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tips = await _safetyTipService.GetAllAsync();
        return Ok(tips);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tip = await _safetyTipService.GetByIdAsync(id);
        if (tip == null) return NotFound();
        return Ok(tip);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateSafetyTipDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tip = await _safetyTipService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = tip.Id }, tip);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSafetyTipDto dto)
    {
        var tip = await _safetyTipService.UpdateAsync(id, dto);
        if (tip == null) return NotFound();
        return Ok(tip);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _safetyTipService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
