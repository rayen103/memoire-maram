using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadSafetyAPI.DTOs.Badge;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Controllers;

[ApiController]
[Route("api/badges")]
[Authorize]
public class BadgeController : ControllerBase
{
    private readonly IBadgeService _badgeService;

    public BadgeController(IBadgeService badgeService)
    {
        _badgeService = badgeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var badges = await _badgeService.GetAllAsync();
        return Ok(badges);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var badge = await _badgeService.GetByIdAsync(id);
        if (badge == null) return NotFound();
        return Ok(badge);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateBadgeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var badge = await _badgeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = badge.Id }, badge);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _badgeService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
