namespace RoadSafetyAPI.DTOs.Quiz;

public class UpdateQuizDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Level { get; set; }
    public int? ScoreMax { get; set; }
}
