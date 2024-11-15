using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Dto
{
    public class WorkSchedule_ReqvestDto : IUserEntity
    {
        public int Id { get; set; }


        public List<DateTime>? StartTime { get; set; }
        public List<DateTime>? EndTime { get; set; }
        public bool busy { get; set; }


        public int UserId { get; set; }
        public List<DateTime>? OnlineTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<DateTime>? OflineTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public User? User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int? IUserEntity.UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}