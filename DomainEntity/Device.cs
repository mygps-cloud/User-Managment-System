namespace Ipstatuschecker.DomainEntity
{
    public class Device
{
    public int? Id { get; set; }
    public string? DeviceNames { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; } 

}
}