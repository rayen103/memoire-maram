using RoadSafetyAPI.DTOs.Common;
using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IVideoRepository
{
    Task<PagedResultDto<Video>> GetAllAsync(int pageNumber, int pageSize);
    Task<Video?> GetByIdAsync(int id);
    Task<Video> CreateAsync(Video video);
    Task UpdateAsync(Video video);
    Task DeleteAsync(int id);
}
