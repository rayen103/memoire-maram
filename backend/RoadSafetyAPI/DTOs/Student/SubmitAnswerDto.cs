using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Student;

public class SubmitAnswerDto
{
    [Required]
    public int StudentProfileId { get; set; }

    [Required]
    public int QuestionId { get; set; }

    [Required]
    public int SelectedAnswerId { get; set; }
}
