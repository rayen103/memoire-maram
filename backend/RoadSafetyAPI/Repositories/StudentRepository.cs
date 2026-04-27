using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StudentProfile?> GetByIdAsync(int id)
    {
        return await _context.StudentProfiles.FindAsync(id);
    }

    public async Task<StudentProfile?> GetByUserIdAsync(int userId)
    {
        return await _context.StudentProfiles
            .Include(sp => sp.User)
            .FirstOrDefaultAsync(sp => sp.UserId == userId);
    }

    public async Task<StudentProfile?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.StudentProfiles
            .Include(sp => sp.User)
            .Include(sp => sp.StudentBadges)
                .ThenInclude(sb => sb.Badge)
            .Include(sp => sp.StudentAnswers)
            .FirstOrDefaultAsync(sp => sp.Id == id);
    }

    public async Task<StudentProfile> CreateAsync(StudentProfile profile)
    {
        _context.StudentProfiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }

    public async Task UpdateAsync(StudentProfile profile)
    {
        _context.StudentProfiles.Update(profile);
        await _context.SaveChangesAsync();
    }

    public async Task<List<StudentAnswer>> GetAnswersByStudentIdAsync(int studentProfileId)
    {
        return await _context.StudentAnswers
            .Where(sa => sa.StudentProfileId == studentProfileId)
            .ToListAsync();
    }

    public async Task<List<StudentAnswer>> GetAnswersByStudentAndQuizAsync(int studentProfileId, int quizId)
    {
        var questionIds = await _context.Questions
            .Where(q => q.QuizId == quizId)
            .Select(q => q.Id)
            .ToListAsync();

        return await _context.StudentAnswers
            .Where(sa => sa.StudentProfileId == studentProfileId && questionIds.Contains(sa.QuestionId))
            .ToListAsync();
    }

    public async Task<StudentAnswer> AddAnswerAsync(StudentAnswer answer)
    {
        _context.StudentAnswers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task<bool> HasAnsweredQuestionAsync(int studentProfileId, int questionId)
    {
        return await _context.StudentAnswers
            .AnyAsync(sa => sa.StudentProfileId == studentProfileId && sa.QuestionId == questionId);
    }

    public async Task<List<StudentBadge>> GetBadgesByStudentIdAsync(int studentProfileId)
    {
        return await _context.StudentBadges
            .Include(sb => sb.Badge)
            .Where(sb => sb.StudentProfileId == studentProfileId)
            .ToListAsync();
    }

    public async Task AddBadgeAsync(StudentBadge badge)
    {
        _context.StudentBadges.Add(badge);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasBadgeAsync(int studentProfileId, int badgeId)
    {
        return await _context.StudentBadges
            .AnyAsync(sb => sb.StudentProfileId == studentProfileId && sb.BadgeId == badgeId);
    }
}
