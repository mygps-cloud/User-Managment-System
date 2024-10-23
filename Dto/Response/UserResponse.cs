namespace Ipstatuschecker.Dto.Response
{public class UserResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<IpStatusResponse>? IpStatuses { get; set; }
    public List<DeviceResponse>? Devices { get; set; }
}

public class IpStatusResponse
{
    public int? Id { get; set; }
    public string? IpAddress { get; set; }
    public string? Status { get; set; }
}

public class DeviceResponse
{
    public int? Id { get; set; }
    public string? DeviceNames { get; set; }
}

}