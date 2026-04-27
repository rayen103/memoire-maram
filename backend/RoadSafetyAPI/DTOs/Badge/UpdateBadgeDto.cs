using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Badge;

public class UpdateBadgeDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int MinPoints { get; set; }
}
