using RoadSafetyAPI.DTOs.Defi;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IDefiService
{
    Task<List<DefiDto>> GetAllAsync();
    Task<DefiDto?> GetByIdAsync(int id);
    Task<DefiDto> CreateAsync(CreateDefiDto dto);
    Task<DefiDto?> UpdateAsync(int id, UpdateDefiDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<StudentDefiDto>> GetStudentDefisAsync(int studentProfileId);
    Task<StudentDefiDto> StartDefiAsync(int studentProfileId, int defiId);
    Task<StudentDefiDto?> CompleteDefiAsync(int studentProfileId, int defiId);
}
