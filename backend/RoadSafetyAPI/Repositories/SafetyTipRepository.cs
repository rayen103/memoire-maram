using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class SafetyTipRepository : ISafetyTipRepository
{
    private readonly AppDbContext _context;

    public SafetyTipRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SafetyTip>> GetAllAsync()
    {
        return await _context.SafetyTips.ToListAsync();
    }

    public async Task<SafetyTip?> GetByIdAsync(int id)
    {
        return await _context.SafetyTips.FindAsync(id);
    }

    public async Task<SafetyTip> CreateAsync(SafetyTip tip)
    {
        _context.SafetyTips.Add(tip);
        await _context.SaveChangesAsync();
        return tip;
    }

    public async Task UpdateAsync(SafetyTip tip)
    {
        _context.SafetyTips.Update(tip);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tip = await _context.SafetyTips.FindAsync(id);
        if (tip != null)
        {
            _context.SafetyTips.Remove(tip);
            await _context.SaveChangesAsync();
        }
    }
}
