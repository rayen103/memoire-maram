using RoadSafetyAPI.DTOs.Correction;

namespace RoadSafetyAPI.Services.Interfaces;

public interface ICorrectionService
{
    Task<CorrectionDto?> GetByQuestionIdAsync(int questionId);
    Task<CorrectionDto> CreateAsync(CreateCorrectionDto dto);
    Task<CorrectionDto?> UpdateAsync(int id, UpdateCorrectionDto dto);
    Task<bool> DeleteAsync(int id);
}
