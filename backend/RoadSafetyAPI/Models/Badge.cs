namespace RoadSafetyAPI.Models;

public class Badge
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinPoints { get; set; }
    public List<StudentBadge> StudentBadges { get; set; } = new();
}
