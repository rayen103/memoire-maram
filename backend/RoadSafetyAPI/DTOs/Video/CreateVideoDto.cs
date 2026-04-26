using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.Video;

public class CreateVideoDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Url { get; set; } = string.Empty;

    public string? Description { get; set; }
}
