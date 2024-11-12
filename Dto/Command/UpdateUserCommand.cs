namespace Ipstatuschecker.Dto.Command
{public class UpdateUserCommand
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<IpStatusCommand>? IpStatuses { get; set; }
    public List<DeviceCommand>? Devices { get; set; }
}

}