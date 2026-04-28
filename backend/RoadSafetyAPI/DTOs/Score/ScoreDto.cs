namespace RoadSafetyAPI.DTOs.Score;

public class ScoreDto
{
    public int Id { get; set; }
    public int StudentProfileId { get; set; }
    public int QuizId { get; set; }
    public string QuizTitle { get; set; } = string.Empty;
    public int ScoreObtenu { get; set; }
    public DateTime Date { get; set; }
}
