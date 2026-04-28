using RoadSafetyAPI.DTOs.Badge;

namespace RoadSafetyAPI.DTOs.Student;

public class StudentProfileDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Avatar { get; set; } = string.Empty;
    public int Points { get; set; }
    public int Level { get; set; }
    public List<BadgeDto> Badges { get; set; } = new();
    public string Name { get; set; } = string.Empty;
}
