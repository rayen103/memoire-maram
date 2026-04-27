using RoadSafetyAPI.DTOs.Question;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IQuestionService
{
    Task<List<QuestionDto>> GetByQuizIdAsync(int quizId);
    Task<QuestionDto?> GetByIdAsync(int id);
    Task<QuestionDto> CreateAsync(CreateQuestionDto dto);
    Task<QuestionDto?> UpdateAsync(int id, UpdateQuestionDto dto);
    Task<bool> DeleteAsync(int id);
}
