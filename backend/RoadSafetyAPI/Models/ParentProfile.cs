namespace RoadSafetyAPI.Models;

public class ParentProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Phone { get; set; } = string.Empty;
    public User User { get; set; } = null!;
}
