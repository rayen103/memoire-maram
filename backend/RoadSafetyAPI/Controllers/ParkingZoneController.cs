using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadSafetyAPI.DTOs.ParkingZone;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Controllers;

[ApiController]
[Route("api/parking-zones")]
[Authorize]
public class ParkingZoneController : ControllerBase
{
    private readonly IParkingZoneService _parkingZoneService;

    public ParkingZoneController(IParkingZoneService parkingZoneService)
    {
        _parkingZoneService = parkingZoneService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var zones = await _parkingZoneService.GetAllAsync();
        return Ok(zones);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var zone = await _parkingZoneService.GetByIdAsync(id);
        if (zone == null) return NotFound();
        return Ok(zone);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateParkingZoneDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var zone = await _parkingZoneService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = zone.Id }, zone);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateParkingZoneDto dto)
    {
        var zone = await _parkingZoneService.UpdateAsync(id, dto);
        if (zone == null) return NotFound();
        return Ok(zone);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _parkingZoneService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
