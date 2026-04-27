using RoadSafetyAPI.DTOs.Common;
using RoadSafetyAPI.DTOs.Video;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IVideoService
{
    Task<PagedResultDto<VideoDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<VideoDto?> GetByIdAsync(int id);
    Task<VideoDto> CreateAsync(CreateVideoDto dto);
    Task<VideoDto?> UpdateAsync(int id, UpdateVideoDto dto);
    Task<bool> DeleteAsync(int id);
}
