namespace RoadSafetyAPI.DTOs.ParkingZone;

public class ParkingZoneDto
{
    public int Id { get; set; }
    public string SchoolName { get; set; } = string.Empty;
    public string ZoneName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
