namespace RoadSafetyAPI.Models;

public class StudentDefi
{
    public int StudentProfileId { get; set; }
    public int DefiId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public StudentProfile StudentProfile { get; set; } = null!;
    public Defi Defi { get; set; } = null!;
}
