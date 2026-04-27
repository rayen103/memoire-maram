using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Answer;

public class CreateAnswerDto
{
    [Required]
    public int QuestionId { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public bool IsCorrect { get; set; }
}
