using RoadSafetyAPI.DTOs.Answer;

namespace RoadSafetyAPI.DTOs.Question;

public class QuestionDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string Content { get; set; } = string.Empty;
    public List<AnswerDto> Answers { get; set; } = new();
}
