using RoadSafetyAPI.DTOs.SafetyTip;

namespace RoadSafetyAPI.Services.Interfaces;

public interface ISafetyTipService
{
    Task<List<SafetyTipDto>> GetAllAsync();
    Task<SafetyTipDto?> GetByIdAsync(int id);
    Task<SafetyTipDto> CreateAsync(CreateSafetyTipDto dto);
    Task<SafetyTipDto?> UpdateAsync(int id, UpdateSafetyTipDto dto);
    Task<bool> DeleteAsync(int id);
}
