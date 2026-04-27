namespace RoadSafetyAPI.DTOs.Student;

public class StudentAnswerDto
{
    public int Id { get; set; }
    public int StudentProfileId { get; set; }
    public int QuestionId { get; set; }
    public int SelectedAnswerId { get; set; }
    public bool IsCorrect { get; set; }
    public DateTime AnsweredAt { get; set; }
}
