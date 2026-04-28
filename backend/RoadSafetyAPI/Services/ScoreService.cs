using RoadSafetyAPI.DTOs.Score;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class ScoreService : IScoreService
{
    private readonly IScoreRepository _scoreRepository;

    public ScoreService(IScoreRepository scoreRepository)
    {
        _scoreRepository = scoreRepository;
    }

    public async Task<List<ScoreDto>> GetByStudentIdAsync(int studentProfileId)
    {
        var scores = await _scoreRepository.GetByStudentIdAsync(studentProfileId);
        return scores.Select(MapToDto).ToList();
    }

    public async Task<ScoreDto> RecordAsync(CreateScoreDto dto)
    {
        var existing = await _scoreRepository.GetByStudentAndQuizAsync(dto.StudentProfileId, dto.QuizId);
        if (existing != null)
        {
            if (dto.ScoreObtenu > existing.ScoreObtenu)
            {
                existing.ScoreObtenu = dto.ScoreObtenu;
                existing.Date = DateTime.UtcNow;
                await _scoreRepository.UpdateAsync(existing);
            }
            return MapToDto(existing);
        }

        var score = new Score
        {
            StudentProfileId = dto.StudentProfileId,
            QuizId = dto.QuizId,
            ScoreObtenu = dto.ScoreObtenu,
            Date = DateTime.UtcNow
        };
        var created = await _scoreRepository.CreateAsync(score);
        return MapToDto(created);
    }

    private static ScoreDto MapToDto(Score score) => new ScoreDto
    {
        Id = score.Id,
        StudentProfileId = score.StudentProfileId,
        QuizId = score.QuizId,
        QuizTitle = score.Quiz?.Title ?? string.Empty,
        ScoreObtenu = score.ScoreObtenu,
        Date = score.Date
    };
}
