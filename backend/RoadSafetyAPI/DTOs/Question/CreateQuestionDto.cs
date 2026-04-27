using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Question;

public class CreateQuestionDto
{
    [Required]
    public int QuizId { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;
}
