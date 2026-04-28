using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class DefiRepository : IDefiRepository
{
    private readonly AppDbContext _context;

    public DefiRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Defi>> GetAllAsync()
    {
        return await _context.Defis.ToListAsync();
    }

    public async Task<Defi?> GetByIdAsync(int id)
    {
        return await _context.Defis.FindAsync(id);
    }

    public async Task<Defi> CreateAsync(Defi defi)
    {
        _context.Defis.Add(defi);
        await _context.SaveChangesAsync();
        return defi;
    }

    public async Task UpdateAsync(Defi defi)
    {
        _context.Defis.Update(defi);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var defi = await _context.Defis.FindAsync(id);
        if (defi != null)
        {
            _context.Defis.Remove(defi);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<StudentDefi>> GetStudentDefisAsync(int studentProfileId)
    {
        return await _context.StudentDefis
            .Include(sd => sd.Defi)
            .Where(sd => sd.StudentProfileId == studentProfileId)
            .ToListAsync();
    }

    public async Task<StudentDefi?> GetStudentDefiAsync(int studentProfileId, int defiId)
    {
        return await _context.StudentDefis
            .Include(sd => sd.Defi)
            .FirstOrDefaultAsync(sd => sd.StudentProfileId == studentProfileId && sd.DefiId == defiId);
    }

    public async Task<StudentDefi> StartDefiAsync(StudentDefi studentDefi)
    {
        _context.StudentDefis.Add(studentDefi);
        await _context.SaveChangesAsync();
        return studentDefi;
    }

    public async Task UpdateStudentDefiAsync(StudentDefi studentDefi)
    {
        _context.StudentDefis.Update(studentDefi);
        await _context.SaveChangesAsync();
    }
}
