namespace RoadSafetyAPI.DTOs.ParkingZone;

public class UpdateParkingZoneDto
{
    public string? SchoolName { get; set; }
    public string? ZoneName { get; set; }
    public string? Type { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
