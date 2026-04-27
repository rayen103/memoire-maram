using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class ParkingZoneRepository : IParkingZoneRepository
{
    private readonly AppDbContext _context;

    public ParkingZoneRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ParkingZone>> GetAllAsync()
    {
        return await _context.ParkingZones.ToListAsync();
    }

    public async Task<ParkingZone?> GetByIdAsync(int id)
    {
        return await _context.ParkingZones.FindAsync(id);
    }

    public async Task<ParkingZone> CreateAsync(ParkingZone zone)
    {
        _context.ParkingZones.Add(zone);
        await _context.SaveChangesAsync();
        return zone;
    }

    public async Task UpdateAsync(ParkingZone zone)
    {
        _context.ParkingZones.Update(zone);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var zone = await _context.ParkingZones.FindAsync(id);
        if (zone != null)
        {
            _context.ParkingZones.Remove(zone);
            await _context.SaveChangesAsync();
        }
    }
}
