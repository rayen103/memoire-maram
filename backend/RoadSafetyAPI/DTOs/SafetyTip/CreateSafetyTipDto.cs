using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.SafetyTip;

public class CreateSafetyTipDto
{
    [Required]
    public string Content { get; set; } = string.Empty;
}
