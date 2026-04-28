using RoadSafetyAPI.DTOs.SafetyTip;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class SafetyTipService : ISafetyTipService
{
    private readonly ISafetyTipRepository _safetyTipRepository;

    public SafetyTipService(ISafetyTipRepository safetyTipRepository)
    {
        _safetyTipRepository = safetyTipRepository;
    }

    public async Task<List<SafetyTipDto>> GetAllAsync()
    {
        var tips = await _safetyTipRepository.GetAllAsync();
        return tips.Select(MapToDto).ToList();
    }

    public async Task<SafetyTipDto?> GetByIdAsync(int id)
    {
        var tip = await _safetyTipRepository.GetByIdAsync(id);
        return tip == null ? null : MapToDto(tip);
    }

    public async Task<SafetyTipDto> CreateAsync(CreateSafetyTipDto dto)
    {
        var tip = new SafetyTip
        {
            Title = dto.Title,
            Description = dto.Description,
            Type = dto.Type ?? string.Empty,
            Image = dto.Image ?? string.Empty,
            Video = dto.Video ?? string.Empty
        };
        var created = await _safetyTipRepository.CreateAsync(tip);
        return MapToDto(created);
    }

    public async Task<SafetyTipDto?> UpdateAsync(int id, UpdateSafetyTipDto dto)
    {
        var tip = await _safetyTipRepository.GetByIdAsync(id);
        if (tip == null) return null;

        if (dto.Title != null) tip.Title = dto.Title;
        if (dto.Description != null) tip.Description = dto.Description;
        if (dto.Type != null) tip.Type = dto.Type;
        if (dto.Image != null) tip.Image = dto.Image;
        if (dto.Video != null) tip.Video = dto.Video;

        await _safetyTipRepository.UpdateAsync(tip);
        return MapToDto(tip);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tip = await _safetyTipRepository.GetByIdAsync(id);
        if (tip == null) return false;

        await _safetyTipRepository.DeleteAsync(id);
        return true;
    }

    private static SafetyTipDto MapToDto(SafetyTip tip) => new SafetyTipDto
    {
        Id = tip.Id,
        Title = tip.Title,
        Description = tip.Description,
        Type = tip.Type,
        Image = tip.Image,
        Video = tip.Video
    };
}
