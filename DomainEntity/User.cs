using System.ComponentModel.DataAnnotations.Schema;

namespace Ipstatuschecker.DomainEntity
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<IpStatus> IpStatuses { get; set; } = new();
        public List<Device> Devices { get; set; } = new();
        public PingLog? PingLog { get; set; }
        public WorkSchedule? workSchedule { get; set; }

    }
}