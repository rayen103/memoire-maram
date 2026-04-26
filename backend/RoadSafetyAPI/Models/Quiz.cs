namespace RoadSafetyAPI.Models;

public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Level { get; set; }
    public List<Question> Questions { get; set; } = new();
}
