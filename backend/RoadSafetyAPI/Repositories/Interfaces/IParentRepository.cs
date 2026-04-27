using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Repositories.Interfaces;

public interface IParentRepository
{
    Task<ParentProfile> CreateAsync(ParentProfile profile);
}
