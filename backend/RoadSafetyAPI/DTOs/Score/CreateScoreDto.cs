using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Score;

public class CreateScoreDto
{
    [Required]
    public int StudentProfileId { get; set; }

    [Required]
    public int QuizId { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int ScoreObtenu { get; set; }
}
