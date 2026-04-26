using RoadSafetyAPI.DTOs.Common;
using RoadSafetyAPI.DTOs.Video;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;

    public VideoService(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }

    public async Task<PagedResultDto<VideoDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var result = await _videoRepository.GetAllAsync(pageNumber, pageSize);
        return new PagedResultDto<VideoDto>
        {
            Items = result.Items.Select(MapToDto).ToList(),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }

    public async Task<VideoDto?> GetByIdAsync(int id)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        return video == null ? null : MapToDto(video);
    }

    public async Task<VideoDto> CreateAsync(CreateVideoDto dto)
    {
        var video = new Video
        {
            Title = dto.Title,
            Url = dto.Url,
            Description = dto.Description ?? string.Empty
        };
        var created = await _videoRepository.CreateAsync(video);
        return MapToDto(created);
    }

    public async Task<VideoDto?> UpdateAsync(int id, UpdateVideoDto dto)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        if (video == null) return null;

        if (dto.Title != null) video.Title = dto.Title;
        if (dto.Url != null) video.Url = dto.Url;
        if (dto.Description != null) video.Description = dto.Description;

        await _videoRepository.UpdateAsync(video);
        return MapToDto(video);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        if (video == null) return false;

        await _videoRepository.DeleteAsync(id);
        return true;
    }

    private static VideoDto MapToDto(Video video) => new VideoDto
    {
        Id = video.Id,
        Title = video.Title,
        Url = video.Url,
        Description = video.Description
    };
}
