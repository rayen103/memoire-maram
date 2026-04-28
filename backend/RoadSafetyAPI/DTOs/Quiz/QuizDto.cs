namespace RoadSafetyAPI.DTOs.Quiz;

public class QuizDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Level { get; set; }
    public int ScoreMax { get; set; }
    public int QuestionCount { get; set; }
}
