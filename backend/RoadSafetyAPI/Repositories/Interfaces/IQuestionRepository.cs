using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IQuestionRepository
{
    Task<List<Question>> GetByQuizIdAsync(int quizId);
    Task<Question?> GetByIdAsync(int id);
    Task<Question?> GetByIdWithAnswersAsync(int id);
    Task<Question> CreateAsync(Question question);
    Task UpdateAsync(Question question);
    Task DeleteAsync(int id);
}
