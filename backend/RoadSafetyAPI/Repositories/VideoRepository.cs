using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.DTOs.Common;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly AppDbContext _context;

    public VideoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResultDto<Video>> GetAllAsync(int pageNumber, int pageSize)
    {
        var totalCount = await _context.Videos.CountAsync();
        var items = await _context.Videos
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResultDto<Video>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<Video?> GetByIdAsync(int id)
    {
        return await _context.Videos.FindAsync(id);
    }

    public async Task<Video> CreateAsync(Video video)
    {
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();
        return video;
    }

    public async Task UpdateAsync(Video video)
    {
        _context.Videos.Update(video);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var video = await _context.Videos.FindAsync(id);
        if (video != null)
        {
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
        }
    }
}
