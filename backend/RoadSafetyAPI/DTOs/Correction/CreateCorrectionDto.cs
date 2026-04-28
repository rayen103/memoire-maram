using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Correction;

public class CreateCorrectionDto
{
    [Required]
    public int QuestionId { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;

    public string? Image { get; set; }
    public string? Video { get; set; }
}
