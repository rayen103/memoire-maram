using RoadSafetyAPI.DTOs.Defi;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class DefiService : IDefiService
{
    private readonly IDefiRepository _defiRepository;
    private readonly IStudentRepository _studentRepository;

    public DefiService(IDefiRepository defiRepository, IStudentRepository studentRepository)
    {
        _defiRepository = defiRepository;
        _studentRepository = studentRepository;
    }

    public async Task<List<DefiDto>> GetAllAsync()
    {
        var defis = await _defiRepository.GetAllAsync();
        return defis.Select(MapToDto).ToList();
    }

    public async Task<DefiDto?> GetByIdAsync(int id)
    {
        var defi = await _defiRepository.GetByIdAsync(id);
        return defi == null ? null : MapToDto(defi);
    }

    public async Task<DefiDto> CreateAsync(CreateDefiDto dto)
    {
        var defi = new Defi
        {
            Title = dto.Title,
            Description = dto.Description ?? string.Empty,
            Objective = dto.Objective,
            PointsGain = dto.PointsGain
        };
        var created = await _defiRepository.CreateAsync(defi);
        return MapToDto(created);
    }

    public async Task<DefiDto?> UpdateAsync(int id, UpdateDefiDto dto)
    {
        var defi = await _defiRepository.GetByIdAsync(id);
        if (defi == null) return null;

        if (dto.Title != null) defi.Title = dto.Title;
        if (dto.Description != null) defi.Description = dto.Description;
        if (dto.Objective != null) defi.Objective = dto.Objective;
        if (dto.PointsGain.HasValue) defi.PointsGain = dto.PointsGain.Value;

        await _defiRepository.UpdateAsync(defi);
        return MapToDto(defi);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var defi = await _defiRepository.GetByIdAsync(id);
        if (defi == null) return false;

        await _defiRepository.DeleteAsync(id);
        return true;
    }

    public async Task<List<StudentDefiDto>> GetStudentDefisAsync(int studentProfileId)
    {
        var studentDefis = await _defiRepository.GetStudentDefisAsync(studentProfileId);
        return studentDefis.Select(MapToStudentDefiDto).ToList();
    }

    public async Task<StudentDefiDto> StartDefiAsync(int studentProfileId, int defiId)
    {
        var existing = await _defiRepository.GetStudentDefiAsync(studentProfileId, defiId);
        if (existing != null)
            return MapToStudentDefiDto(existing);

        var studentDefi = new StudentDefi
        {
            StudentProfileId = studentProfileId,
            DefiId = defiId,
            IsCompleted = false,
            StartedAt = DateTime.UtcNow
        };
        var created = await _defiRepository.StartDefiAsync(studentDefi);
        created.Defi = (await _defiRepository.GetByIdAsync(defiId))!;
        return MapToStudentDefiDto(created);
    }

    public async Task<StudentDefiDto?> CompleteDefiAsync(int studentProfileId, int defiId)
    {
        var studentDefi = await _defiRepository.GetStudentDefiAsync(studentProfileId, defiId);
        if (studentDefi == null) return null;

        if (!studentDefi.IsCompleted)
        {
            studentDefi.IsCompleted = true;
            studentDefi.CompletedAt = DateTime.UtcNow;
            await _defiRepository.UpdateStudentDefiAsync(studentDefi);

            var profile = await _studentRepository.GetByIdAsync(studentProfileId);
            if (profile != null)
            {
                profile.Points += studentDefi.Defi.PointsGain;
                await _studentRepository.UpdateAsync(profile);
            }
        }

        return MapToStudentDefiDto(studentDefi);
    }

    private static DefiDto MapToDto(Defi defi) => new DefiDto
    {
        Id = defi.Id,
        Title = defi.Title,
        Description = defi.Description,
        Objective = defi.Objective,
        PointsGain = defi.PointsGain
    };

    private static StudentDefiDto MapToStudentDefiDto(StudentDefi sd) => new StudentDefiDto
    {
        DefiId = sd.DefiId,
        Title = sd.Defi?.Title ?? string.Empty,
        Description = sd.Defi?.Description ?? string.Empty,
        Objective = sd.Defi?.Objective ?? string.Empty,
        PointsGain = sd.Defi?.PointsGain ?? 0,
        IsCompleted = sd.IsCompleted,
        StartedAt = sd.StartedAt,
        CompletedAt = sd.CompletedAt
    };
}
