namespace RoadSafetyAPI.Models;

public class Correction
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Video { get; set; } = string.Empty;
    public Question Question { get; set; } = null!;
}
