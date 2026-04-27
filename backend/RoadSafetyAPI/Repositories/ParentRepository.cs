using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class ParentRepository : IParentRepository
{
    private readonly AppDbContext _context;

    public ParentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ParentProfile> CreateAsync(ParentProfile profile)
    {
        _context.ParentProfiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }
}
