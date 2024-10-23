namespace Ipstatuschecker.DomainEntity
{
    public class User
    {
        public int Id {get ;set;}
        public string? Name {get;set;}
       public List<IpStatus>? IpStatuses { get; set; } = new ();
       public List<Device>? Devices { get; set; } = new ();
    }
}