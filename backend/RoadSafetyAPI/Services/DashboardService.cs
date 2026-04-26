using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.DTOs.Dashboard;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardStatsDto> GetStatsAsync()
    {
        var totalUsers = await _context.Users.CountAsync();
        var totalStudents = await _context.Users.CountAsync(u => u.Role == "STUDENT");
        var totalParents = await _context.Users.CountAsync(u => u.Role == "PARENT");
        var totalQuizzes = await _context.Quizzes.CountAsync();
        var totalVideos = await _context.Videos.CountAsync();

        var totalAnswers = await _context.StudentAnswers.CountAsync();
        var correctAnswers = await _context.StudentAnswers.CountAsync(sa => sa.IsCorrect);
        var averageScore = totalAnswers > 0 ? (double)correctAnswers / totalAnswers * 100 : 0;

        return new DashboardStatsDto
        {
            TotalUsers = totalUsers,
            TotalStudents = totalStudents,
            TotalParents = totalParents,
            TotalQuizzes = totalQuizzes,
            TotalVideos = totalVideos,
            AverageScore = averageScore
        };
    }
}
