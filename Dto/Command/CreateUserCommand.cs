namespace Ipstatuschecker.Dto.Command
{public class CreateUserCommand
{
    public string? Name { get; set; }
    public List<IpStatusCommand>? IpStatuses { get; set; }
    public List<DeviceCommand>? Devices { get; set; }
}

public class IpStatusCommand
{
    public string? IpAddress { get; set; }
    public string? Status { get; set; }
}

public class DeviceCommand
{
    public string? DeviceNames { get; set; }
}


}