using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IBadgeRepository
{
    Task<List<Badge>> GetAllAsync();
    Task<Badge?> GetByIdAsync(int id);
    Task<List<Badge>> GetEarnableByPointsAsync(int points);
    Task<Badge> CreateAsync(Badge badge);
    Task UpdateAsync(Badge badge);
    Task DeleteAsync(int id);
}
