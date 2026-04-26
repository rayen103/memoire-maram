namespace RoadSafetyAPI.DTOs.Dashboard;

public class DashboardStatsDto
{
    public int TotalUsers { get; set; }
    public int TotalStudents { get; set; }
    public int TotalParents { get; set; }
    public int TotalQuizzes { get; set; }
    public int TotalVideos { get; set; }
    public double AverageScore { get; set; }
}
