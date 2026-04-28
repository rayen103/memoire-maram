using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class ScoreRepository : IScoreRepository
{
    private readonly AppDbContext _context;

    public ScoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Score>> GetByStudentIdAsync(int studentProfileId)
    {
        return await _context.Scores
            .Include(s => s.Quiz)
            .Where(s => s.StudentProfileId == studentProfileId)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<Score?> GetByStudentAndQuizAsync(int studentProfileId, int quizId)
    {
        return await _context.Scores
            .Include(s => s.Quiz)
            .FirstOrDefaultAsync(s => s.StudentProfileId == studentProfileId && s.QuizId == quizId);
    }

    public async Task<Score> CreateAsync(Score score)
    {
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
        return score;
    }

    public async Task UpdateAsync(Score score)
    {
        _context.Scores.Update(score);
        await _context.SaveChangesAsync();
    }
}
