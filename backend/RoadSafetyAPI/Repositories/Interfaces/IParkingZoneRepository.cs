using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IParkingZoneRepository
{
    Task<List<ParkingZone>> GetAllAsync();
    Task<ParkingZone?> GetByIdAsync(int id);
    Task<ParkingZone> CreateAsync(ParkingZone zone);
    Task UpdateAsync(ParkingZone zone);
    Task DeleteAsync(int id);
}
