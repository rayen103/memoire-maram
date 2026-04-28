using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Defi;

public class CreateDefiDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public string Objective { get; set; } = string.Empty;

    [Required]
    [Range(0, int.MaxValue)]
    public int PointsGain { get; set; }
}
