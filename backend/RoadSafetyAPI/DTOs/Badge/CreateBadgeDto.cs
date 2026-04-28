using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Badge;

public class CreateBadgeDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Image { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int MinPoints { get; set; }
}
