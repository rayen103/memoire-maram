using RoadSafetyAPI.DTOs.Common;
using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IQuizRepository
{
    Task<PagedResultDto<Quiz>> GetAllAsync(int pageNumber, int pageSize);
    Task<Quiz?> GetByIdAsync(int id);
    Task<Quiz?> GetByIdWithQuestionsAsync(int id);
    Task<List<Quiz>> GetByLevelAsync(int level);
    Task<Quiz> CreateAsync(Quiz quiz);
    Task UpdateAsync(Quiz quiz);
    Task DeleteAsync(int id);
}
