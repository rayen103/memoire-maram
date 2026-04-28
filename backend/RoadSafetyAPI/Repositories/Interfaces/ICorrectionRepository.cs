using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface ICorrectionRepository
{
    Task<Correction?> GetByQuestionIdAsync(int questionId);
    Task<Correction?> GetByIdAsync(int id);
    Task<Correction> CreateAsync(Correction correction);
    Task UpdateAsync(Correction correction);
    Task DeleteAsync(int id);
}
