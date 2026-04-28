namespace RoadSafetyAPI.Models;

public class Defi
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Objective { get; set; } = string.Empty;
    public int PointsGain { get; set; }
    public List<StudentDefi> StudentDefis { get; set; } = new();
}
