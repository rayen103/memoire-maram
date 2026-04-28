using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Question;

public class CreateQuestionDto
{
    [Required]
    public int QuizId { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public string? Type { get; set; }
    public string? Image { get; set; }
    public string? Explication { get; set; }
}
