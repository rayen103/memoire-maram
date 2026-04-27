using RoadSafetyAPI.DTOs.ParkingZone;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IParkingZoneService
{
    Task<List<ParkingZoneDto>> GetAllAsync();
    Task<ParkingZoneDto?> GetByIdAsync(int id);
    Task<ParkingZoneDto> CreateAsync(CreateParkingZoneDto dto);
    Task<ParkingZoneDto?> UpdateAsync(int id, UpdateParkingZoneDto dto);
    Task<bool> DeleteAsync(int id);
}
