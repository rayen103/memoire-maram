using RoadSafetyAPI.DTOs.Dashboard;

namespace RoadSafetyAPI.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetStatsAsync();
}
