using RoadSafetyAPI.DTOs.Common;
using RoadSafetyAPI.DTOs.Quiz;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IQuizService
{
    Task<PagedResultDto<QuizDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<QuizDto?> GetByIdAsync(int id);
    Task<List<QuizDto>> GetByLevelAsync(int level);
    Task<QuizDto> CreateAsync(CreateQuizDto dto);
    Task<QuizDto?> UpdateAsync(int id, UpdateQuizDto dto);
    Task<bool> DeleteAsync(int id);
}
