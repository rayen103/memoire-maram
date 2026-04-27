namespace RoadSafetyAPI.Models;

public class Question
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Quiz Quiz { get; set; } = null!;
    public List<Answer> Answers { get; set; } = new();
}
