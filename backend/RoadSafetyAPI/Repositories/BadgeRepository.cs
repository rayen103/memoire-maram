using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class BadgeRepository : IBadgeRepository
{
    private readonly AppDbContext _context;

    public BadgeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Badge>> GetAllAsync()
    {
        return await _context.Badges.ToListAsync();
    }

    public async Task<Badge?> GetByIdAsync(int id)
    {
        return await _context.Badges.FindAsync(id);
    }

    public async Task<List<Badge>> GetEarnableByPointsAsync(int points)
    {
        return await _context.Badges
            .Where(b => b.MinPoints <= points)
            .ToListAsync();
    }

    public async Task<Badge> CreateAsync(Badge badge)
    {
        _context.Badges.Add(badge);
        await _context.SaveChangesAsync();
        return badge;
    }

    public async Task UpdateAsync(Badge badge)
    {
        _context.Badges.Update(badge);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var badge = await _context.Badges.FindAsync(id);
        if (badge != null)
        {
            _context.Badges.Remove(badge);
            await _context.SaveChangesAsync();
        }
    }
}
