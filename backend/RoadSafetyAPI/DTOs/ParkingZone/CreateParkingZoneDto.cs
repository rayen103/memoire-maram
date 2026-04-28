using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.ParkingZone;

public class CreateParkingZoneDto
{
    [Required]
    public string SchoolName { get; set; } = string.Empty;

    public string? ZoneName { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
