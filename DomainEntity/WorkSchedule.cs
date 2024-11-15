using System.Collections.Generic;


namespace Ipstatuschecker.DomainEntity
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        public List<DateTime>? StartTime { get; set; }
        public List<DateTime>? EndTime { get; set; } 
        public bool Notification{ get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }

}