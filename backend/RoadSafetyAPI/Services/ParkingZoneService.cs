using RoadSafetyAPI.DTOs.ParkingZone;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class ParkingZoneService : IParkingZoneService
{
    private readonly IParkingZoneRepository _parkingZoneRepository;

    public ParkingZoneService(IParkingZoneRepository parkingZoneRepository)
    {
        _parkingZoneRepository = parkingZoneRepository;
    }

    public async Task<List<ParkingZoneDto>> GetAllAsync()
    {
        var zones = await _parkingZoneRepository.GetAllAsync();
        return zones.Select(MapToDto).ToList();
    }

    public async Task<ParkingZoneDto?> GetByIdAsync(int id)
    {
        var zone = await _parkingZoneRepository.GetByIdAsync(id);
        return zone == null ? null : MapToDto(zone);
    }

    public async Task<ParkingZoneDto> CreateAsync(CreateParkingZoneDto dto)
    {
        var zone = new ParkingZone
        {
            SchoolName = dto.SchoolName,
            ZoneName = dto.ZoneName ?? string.Empty,
            Type = dto.Type,
            Location = dto.Location,
            Description = dto.Description ?? string.Empty,
            Latitude = dto.Latitude ?? 0,
            Longitude = dto.Longitude ?? 0
        };
        var created = await _parkingZoneRepository.CreateAsync(zone);
        return MapToDto(created);
    }

    public async Task<ParkingZoneDto?> UpdateAsync(int id, UpdateParkingZoneDto dto)
    {
        var zone = await _parkingZoneRepository.GetByIdAsync(id);
        if (zone == null) return null;

        if (dto.SchoolName != null) zone.SchoolName = dto.SchoolName;
        if (dto.ZoneName != null) zone.ZoneName = dto.ZoneName;
        if (dto.Type != null) zone.Type = dto.Type;
        if (dto.Location != null) zone.Location = dto.Location;
        if (dto.Description != null) zone.Description = dto.Description;
        if (dto.Latitude.HasValue) zone.Latitude = dto.Latitude.Value;
        if (dto.Longitude.HasValue) zone.Longitude = dto.Longitude.Value;

        await _parkingZoneRepository.UpdateAsync(zone);
        return MapToDto(zone);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var zone = await _parkingZoneRepository.GetByIdAsync(id);
        if (zone == null) return false;

        await _parkingZoneRepository.DeleteAsync(id);
        return true;
    }

    private static ParkingZoneDto MapToDto(ParkingZone zone) => new ParkingZoneDto
    {
        Id = zone.Id,
        SchoolName = zone.SchoolName,
        ZoneName = zone.ZoneName,
        Type = zone.Type,
        Location = zone.Location,
        Description = zone.Description,
        Latitude = zone.Latitude,
        Longitude = zone.Longitude
    };
}
