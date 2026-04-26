namespace RoadSafetyAPI.Models;

public class StudentProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Points { get; set; } = 0;
    public int Level { get; set; } = 1;
    public User User { get; set; } = null!;
    public List<StudentAnswer> StudentAnswers { get; set; } = new();
    public List<StudentBadge> StudentBadges { get; set; } = new();
}
