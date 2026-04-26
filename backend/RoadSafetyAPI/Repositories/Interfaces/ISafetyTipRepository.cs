using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface ISafetyTipRepository
{
    Task<List<SafetyTip>> GetAllAsync();
    Task<SafetyTip?> GetByIdAsync(int id);
    Task<SafetyTip> CreateAsync(SafetyTip tip);
    Task UpdateAsync(SafetyTip tip);
    Task DeleteAsync(int id);
}
