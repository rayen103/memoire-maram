using RoadSafetyAPI.DTOs.Badge;
using RoadSafetyAPI.DTOs.Student;
using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IStudentService
{
    Task<StudentProfileDto?> GetProfileAsync(int studentProfileId);
    Task<StudentProfileDto?> GetProfileByUserIdAsync(int userId);
    Task<List<StudentAnswerDto>> GetAnswersAsync(int studentProfileId);
    Task<List<BadgeDto>> GetBadgesAsync(int studentProfileId);
    Task<QuizResultDto?> GetQuizResultAsync(int studentProfileId, int quizId);
    Task<StudentAnswer?> SubmitAnswerAsync(SubmitAnswerDto dto);
}
