using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.DTOs.Badge;
using RoadSafetyAPI.DTOs.Student;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IBadgeRepository _badgeRepository;
    private readonly AppDbContext _context;

    public StudentService(
        IStudentRepository studentRepository,
        IAnswerRepository answerRepository,
        IQuestionRepository questionRepository,
        IBadgeRepository badgeRepository,
        AppDbContext context)
    {
        _studentRepository = studentRepository;
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _badgeRepository = badgeRepository;
        _context = context;
    }

    public async Task<StudentProfileDto?> GetProfileAsync(int studentProfileId)
    {
        var profile = await _studentRepository.GetByIdWithDetailsAsync(studentProfileId);
        if (profile == null) return null;
        return MapToDto(profile);
    }

    public async Task<StudentProfileDto?> GetProfileByUserIdAsync(int userId)
    {
        var profile = await _studentRepository.GetByUserIdAsync(userId);
        if (profile == null) return null;

        var detailed = await _studentRepository.GetByIdWithDetailsAsync(profile.Id);
        if (detailed == null) return null;
        return MapToDto(detailed);
    }

    public async Task<List<BadgeDto>> GetBadgesAsync(int studentProfileId)
    {
        var badges = await _studentRepository.GetBadgesByStudentIdAsync(studentProfileId);
        return badges.Select(sb => new BadgeDto
        {
            Id = sb.Badge.Id,
            Name = sb.Badge.Name,
            Description = sb.Badge.Description,
            MinPoints = sb.Badge.MinPoints
        }).ToList();
    }

    public async Task<List<StudentAnswerDto>> GetAnswersAsync(int studentProfileId)
    {
        var answers = await _studentRepository.GetAnswersByStudentIdAsync(studentProfileId);
        return answers.Select(sa => new StudentAnswerDto
        {
            Id = sa.Id,
            StudentProfileId = sa.StudentProfileId,
            QuestionId = sa.QuestionId,
            SelectedAnswerId = sa.SelectedAnswerId,
            IsCorrect = sa.IsCorrect,
            AnsweredAt = sa.AnsweredAt
        }).ToList();
    }

    public async Task<QuizResultDto?> GetQuizResultAsync(int studentProfileId, int quizId)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == quizId);

        if (quiz == null) return null;

        var studentAnswers = await _studentRepository.GetAnswersByStudentAndQuizAsync(studentProfileId, quizId);
        if (!studentAnswers.Any()) return null;

        var answerDetails = new List<StudentAnswerDetailDto>();

        foreach (var sa in studentAnswers)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == sa.QuestionId);
            if (question == null) continue;

            var selectedAnswer = question.Answers.FirstOrDefault(a => a.Id == sa.SelectedAnswerId);
            var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);

            answerDetails.Add(new StudentAnswerDetailDto
            {
                QuestionContent = question.Content,
                SelectedAnswerContent = selectedAnswer?.Content ?? string.Empty,
                IsCorrect = sa.IsCorrect,
                CorrectAnswerContent = correctAnswer?.Content ?? string.Empty
            });
        }

        int correctCount = studentAnswers.Count(sa => sa.IsCorrect);
        int totalQuestions = quiz.Questions.Count;
        double score = totalQuestions > 0 ? (double)correctCount / totalQuestions * 100 : 0;

        return new QuizResultDto
        {
            QuizId = quiz.Id,
            QuizTitle = quiz.Title,
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctCount,
            Score = score,
            Answers = answerDetails
        };
    }

    public async Task<StudentAnswer?> SubmitAnswerAsync(SubmitAnswerDto dto)
    {
        var alreadyAnswered = await _studentRepository.HasAnsweredQuestionAsync(dto.StudentProfileId, dto.QuestionId);
        if (alreadyAnswered)
            throw new InvalidOperationException("Student has already answered this question.");

        var answer = await _answerRepository.GetByIdAsync(dto.SelectedAnswerId);
        if (answer == null) return null;

        var isCorrect = answer.IsCorrect;

        var studentAnswer = new StudentAnswer
        {
            StudentProfileId = dto.StudentProfileId,
            QuestionId = dto.QuestionId,
            SelectedAnswerId = dto.SelectedAnswerId,
            IsCorrect = isCorrect,
            AnsweredAt = DateTime.UtcNow
        };

        await _studentRepository.AddAnswerAsync(studentAnswer);

        if (isCorrect)
        {
            var profile = await _studentRepository.GetByIdAsync(dto.StudentProfileId);
            if (profile != null)
            {
                profile.Points += 10;
                profile.Level = (profile.Points / 50) + 1;
                await _studentRepository.UpdateAsync(profile);

                var earnableBadges = await _badgeRepository.GetEarnableByPointsAsync(profile.Points);
                foreach (var badge in earnableBadges)
                {
                    var hasBadge = await _studentRepository.HasBadgeAsync(dto.StudentProfileId, badge.Id);
                    if (!hasBadge)
                    {
                        await _studentRepository.AddBadgeAsync(new StudentBadge
                        {
                            StudentProfileId = dto.StudentProfileId,
                            BadgeId = badge.Id,
                            EarnedAt = DateTime.UtcNow
                        });
                    }
                }
            }
        }

        return studentAnswer;
    }

    private static StudentProfileDto MapToDto(StudentProfile profile) => new StudentProfileDto
    {
        Id = profile.Id,
        UserId = profile.UserId,
        Points = profile.Points,
        Level = profile.Level,
        Name = profile.User?.Name ?? string.Empty,
        Badges = profile.StudentBadges?.Select(sb => new BadgeDto
        {
            Id = sb.Badge.Id,
            Name = sb.Badge.Name,
            Description = sb.Badge.Description,
            MinPoints = sb.Badge.MinPoints
        }).ToList() ?? new List<BadgeDto>()
    };
}
