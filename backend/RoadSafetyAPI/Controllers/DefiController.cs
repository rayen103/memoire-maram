using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadSafetyAPI.DTOs.Defi;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Controllers;

[ApiController]
[Route("api/defis")]
[Authorize]
public class DefiController : ControllerBase
{
    private readonly IDefiService _defiService;

    public DefiController(IDefiService defiService)
    {
        _defiService = defiService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var defis = await _defiService.GetAllAsync();
        return Ok(defis);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var defi = await _defiService.GetByIdAsync(id);
        if (defi == null) return NotFound();
        return Ok(defi);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateDefiDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var defi = await _defiService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = defi.Id }, defi);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDefiDto dto)
    {
        var defi = await _defiService.UpdateAsync(id, dto);
        if (defi == null) return NotFound();
        return Ok(defi);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _defiService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpGet("student/{studentProfileId}")]
    public async Task<IActionResult> GetStudentDefis(int studentProfileId)
    {
        var defis = await _defiService.GetStudentDefisAsync(studentProfileId);
        return Ok(defis);
    }

    [HttpPost("student/{studentProfileId}/start/{defiId}")]
    public async Task<IActionResult> Start(int studentProfileId, int defiId)
    {
        var studentDefi = await _defiService.StartDefiAsync(studentProfileId, defiId);
        return Ok(studentDefi);
    }

    [HttpPost("student/{studentProfileId}/complete/{defiId}")]
    public async Task<IActionResult> Complete(int studentProfileId, int defiId)
    {
        var studentDefi = await _defiService.CompleteDefiAsync(studentProfileId, defiId);
        if (studentDefi == null) return NotFound();
        return Ok(studentDefi);
    }
}
