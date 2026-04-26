using RoadSafetyAPI.DTOs.Badge;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IBadgeService
{
    Task<List<BadgeDto>> GetAllAsync();
    Task<BadgeDto?> GetByIdAsync(int id);
    Task<BadgeDto> CreateAsync(CreateBadgeDto dto);
    Task<bool> DeleteAsync(int id);
}
