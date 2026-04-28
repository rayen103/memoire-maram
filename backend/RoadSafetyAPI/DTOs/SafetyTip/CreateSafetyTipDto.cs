using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.SafetyTip;

public class CreateSafetyTipDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public string? Type { get; set; }
    public string? Image { get; set; }
    public string? Video { get; set; }
}
