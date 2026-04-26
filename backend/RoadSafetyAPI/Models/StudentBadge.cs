namespace RoadSafetyAPI.Models;

public class StudentBadge
{
    public int StudentProfileId { get; set; }
    public int BadgeId { get; set; }
    public DateTime EarnedAt { get; set; }
    public StudentProfile StudentProfile { get; set; } = null!;
    public Badge Badge { get; set; } = null!;
}
