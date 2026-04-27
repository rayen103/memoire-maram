using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IAnswerRepository
{
    Task<List<Answer>> GetByQuestionIdAsync(int questionId);
    Task<Answer?> GetByIdAsync(int id);
    Task<Answer> CreateAsync(Answer answer);
    Task UpdateAsync(Answer answer);
    Task DeleteAsync(int id);
}
