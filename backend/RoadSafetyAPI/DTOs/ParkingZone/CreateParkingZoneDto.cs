using System.ComponentModel.DataAnnotations;

namespace RoadSafetyAPI.DTOs.ParkingZone;

public class CreateParkingZoneDto
{
    [Required]
    public string SchoolName { get; set; } = string.Empty;

    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;
}
