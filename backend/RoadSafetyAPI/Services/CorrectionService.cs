using RoadSafetyAPI.DTOs.Correction;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class CorrectionService : ICorrectionService
{
    private readonly ICorrectionRepository _correctionRepository;

    public CorrectionService(ICorrectionRepository correctionRepository)
    {
        _correctionRepository = correctionRepository;
    }

    public async Task<CorrectionDto?> GetByQuestionIdAsync(int questionId)
    {
        var correction = await _correctionRepository.GetByQuestionIdAsync(questionId);
        return correction == null ? null : MapToDto(correction);
    }

    public async Task<CorrectionDto> CreateAsync(CreateCorrectionDto dto)
    {
        var correction = new Correction
        {
            QuestionId = dto.QuestionId,
            Text = dto.Text,
            Image = dto.Image ?? string.Empty,
            Video = dto.Video ?? string.Empty
        };
        var created = await _correctionRepository.CreateAsync(correction);
        return MapToDto(created);
    }

    public async Task<CorrectionDto?> UpdateAsync(int id, UpdateCorrectionDto dto)
    {
        var correction = await _correctionRepository.GetByIdAsync(id);
        if (correction == null) return null;

        if (dto.Text != null) correction.Text = dto.Text;
        if (dto.Image != null) correction.Image = dto.Image;
        if (dto.Video != null) correction.Video = dto.Video;

        await _correctionRepository.UpdateAsync(correction);
        return MapToDto(correction);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var correction = await _correctionRepository.GetByIdAsync(id);
        if (correction == null) return false;

        await _correctionRepository.DeleteAsync(id);
        return true;
    }

    private static CorrectionDto MapToDto(Correction correction) => new CorrectionDto
    {
        Id = correction.Id,
        QuestionId = correction.QuestionId,
        Text = correction.Text,
        Image = correction.Image,
        Video = correction.Video
    };
}
