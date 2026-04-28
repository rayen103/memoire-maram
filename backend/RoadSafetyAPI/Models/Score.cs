namespace RoadSafetyAPI.Models;

public class Score
{
    public int Id { get; set; }
    public int StudentProfileId { get; set; }
    public int QuizId { get; set; }
    public int ScoreObtenu { get; set; }
    public DateTime Date { get; set; }
    public StudentProfile StudentProfile { get; set; } = null!;
    public Quiz Quiz { get; set; } = null!;
}
