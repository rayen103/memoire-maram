using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IDefiRepository
{
    Task<List<Defi>> GetAllAsync();
    Task<Defi?> GetByIdAsync(int id);
    Task<Defi> CreateAsync(Defi defi);
    Task UpdateAsync(Defi defi);
    Task DeleteAsync(int id);
    Task<List<StudentDefi>> GetStudentDefisAsync(int studentProfileId);
    Task<StudentDefi?> GetStudentDefiAsync(int studentProfileId, int defiId);
    Task<StudentDefi> StartDefiAsync(StudentDefi studentDefi);
    Task UpdateStudentDefiAsync(StudentDefi studentDefi);
}
