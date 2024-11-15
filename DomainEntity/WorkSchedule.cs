using System.ComponentModel.DataAnnotations;

namespace Ipstatuschecker.DomainEntity
{
    public class WorkSchedule
    {
        public int Id { get; set; }

        public List<DateTime>? StartTime { get; set; }

        public List<DateTime>? EndTime { get; set; }

        public bool busy { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
