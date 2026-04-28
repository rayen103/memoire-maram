using RoadSafetyAPI.DTOs.Score;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IScoreService
{
    Task<List<ScoreDto>> GetByStudentIdAsync(int studentProfileId);
    Task<ScoreDto> RecordAsync(CreateScoreDto dto);
}
