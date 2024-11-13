
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Dto.Command
{
    public class WorkScheduleReqvestAdapter : IUserEntity
    {
        public int UserId { get; set; }
        public WorkSchedule_ReqvestDto InnerDto { get; }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<DateTime>? OnlineTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<DateTime>? OflineTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int? IUserEntity.UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public User? User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WorkScheduleReqvestAdapter(WorkSchedule_ReqvestDto dto)
        {
            InnerDto = dto;
            UserId = dto.UserId;
        }
    }

}