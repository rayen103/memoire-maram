namespace RoadSafetyAPI.DTOs.Badge;

public class BadgeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MinPoints { get; set; }
}
