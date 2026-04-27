using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IStudentRepository
{
    Task<StudentProfile?> GetByIdAsync(int id);
    Task<StudentProfile?> GetByUserIdAsync(int userId);
    Task<StudentProfile?> GetByIdWithDetailsAsync(int id);
    Task<StudentProfile> CreateAsync(StudentProfile profile);
    Task UpdateAsync(StudentProfile profile);
    Task<List<StudentAnswer>> GetAnswersByStudentIdAsync(int studentProfileId);
    Task<List<StudentAnswer>> GetAnswersByStudentAndQuizAsync(int studentProfileId, int quizId);
    Task<StudentAnswer> AddAnswerAsync(StudentAnswer answer);
    Task<bool> HasAnsweredQuestionAsync(int studentProfileId, int questionId);
    Task<List<StudentBadge>> GetBadgesByStudentIdAsync(int studentProfileId);
    Task AddBadgeAsync(StudentBadge badge);
    Task<bool> HasBadgeAsync(int studentProfileId, int badgeId);
}
