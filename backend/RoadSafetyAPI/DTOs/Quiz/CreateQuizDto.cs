using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Quiz;

public class CreateQuizDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(1, 10)]
    public int Level { get; set; }
}
