using RoadSafetyAPI.DTOs.Badge;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class BadgeService : IBadgeService
{
    private readonly IBadgeRepository _badgeRepository;

    public BadgeService(IBadgeRepository badgeRepository)
    {
        _badgeRepository = badgeRepository;
    }

    public async Task<List<BadgeDto>> GetAllAsync()
    {
        var badges = await _badgeRepository.GetAllAsync();
        return badges.Select(MapToDto).ToList();
    }

    public async Task<BadgeDto?> GetByIdAsync(int id)
    {
        var badge = await _badgeRepository.GetByIdAsync(id);
        return badge == null ? null : MapToDto(badge);
    }

    public async Task<BadgeDto> CreateAsync(CreateBadgeDto dto)
    {
        var badge = new Badge
        {
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            MinPoints = dto.MinPoints
        };
        var created = await _badgeRepository.CreateAsync(badge);
        return MapToDto(created);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var badge = await _badgeRepository.GetByIdAsync(id);
        if (badge == null) return false;

        await _badgeRepository.DeleteAsync(id);
        return true;
    }

    private static BadgeDto MapToDto(Badge badge) => new BadgeDto
    {
        Id = badge.Id,
        Name = badge.Name,
        Description = badge.Description,
        MinPoints = badge.MinPoints
    };
}
