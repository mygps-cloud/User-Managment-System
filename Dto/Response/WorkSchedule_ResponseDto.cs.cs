using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Dto
{
    public class WorkSchedule_ResponseDto
    {
       public int Id { get; set; }
        public List<DateTime>? StartTime { get; set; }
        public List<DateTime>? EndTime { get; set; }
        public bool Notification;
        public int? UserId { get; set; }
        public User? User { get; set; }


    
    }
}