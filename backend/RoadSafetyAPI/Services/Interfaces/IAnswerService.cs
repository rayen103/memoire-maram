using RoadSafetyAPI.DTOs.Answer;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IAnswerService
{
    Task<List<AnswerDto>> GetByQuestionIdAsync(int questionId);
    Task<AnswerDto?> GetByIdAsync(int id);
    Task<AnswerDto> CreateAsync(CreateAnswerDto dto);
    Task<AnswerDto?> UpdateAsync(int id, UpdateAnswerDto dto);
    Task<bool> DeleteAsync(int id);
}
