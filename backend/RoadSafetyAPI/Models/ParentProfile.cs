namespace RoadSafetyAPI.Models;

public class ParentProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
