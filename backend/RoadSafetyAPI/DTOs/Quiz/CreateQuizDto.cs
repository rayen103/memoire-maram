using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Quiz;

public class CreateQuizDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [Range(1, 10)]
    public int Level { get; set; }

    [Range(0, int.MaxValue)]
    public int ScoreMax { get; set; }
}
