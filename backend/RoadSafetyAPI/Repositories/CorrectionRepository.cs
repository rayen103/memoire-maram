using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class CorrectionRepository : ICorrectionRepository
{
    private readonly AppDbContext _context;

    public CorrectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Correction?> GetByQuestionIdAsync(int questionId)
    {
        return await _context.Corrections.FirstOrDefaultAsync(c => c.QuestionId == questionId);
    }

    public async Task<Correction?> GetByIdAsync(int id)
    {
        return await _context.Corrections.FindAsync(id);
    }

    public async Task<Correction> CreateAsync(Correction correction)
    {
        _context.Corrections.Add(correction);
        await _context.SaveChangesAsync();
        return correction;
    }

    public async Task UpdateAsync(Correction correction)
    {
        _context.Corrections.Update(correction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var correction = await _context.Corrections.FindAsync(id);
        if (correction != null)
        {
            _context.Corrections.Remove(correction);
            await _context.SaveChangesAsync();
        }
    }
}
