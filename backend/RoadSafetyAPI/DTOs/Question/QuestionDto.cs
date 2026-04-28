using RoadSafetyAPI.DTOs.Answer;
using RoadSafetyAPI.DTOs.Correction;

namespace RoadSafetyAPI.DTOs.Question;

public class QuestionDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Explication { get; set; } = string.Empty;
    public List<AnswerDto> Answers { get; set; } = new();
    public CorrectionDto? Correction { get; set; }
}
