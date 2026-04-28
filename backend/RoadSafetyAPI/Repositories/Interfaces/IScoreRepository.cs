using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IScoreRepository
{
    Task<List<Score>> GetByStudentIdAsync(int studentProfileId);
    Task<Score?> GetByStudentAndQuizAsync(int studentProfileId, int quizId);
    Task<Score> CreateAsync(Score score);
    Task UpdateAsync(Score score);
}
