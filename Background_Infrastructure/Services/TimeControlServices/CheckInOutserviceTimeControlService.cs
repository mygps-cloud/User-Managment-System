using ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Background_Infrastructure.Services.TimeControlServices
{
    public class CheckInOutserviceTimeControlService : ITimeControl<PingLogDtoReqvest, CheckInOutServiceResult>
    {
        public Task<CheckInOutServiceResult> TimeControlResult(PingLogDtoReqvest entity, bool Status)
        {
            var Result = new CheckInOutServiceResult
            {
                HasOnlineRecordForToday = entity?.OnlineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false,
                
                HasSufficientTimePassed = entity?.OnlineTime?.Count > 0 && 
                                          entity.OnlineTime?.Any(time => time.Day == DateTime.Now.Day) == true,
                
                HasOfflineRecordForToday = entity?.OflineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false,
                
                LastTimeIn = entity?.OnlineTime != null && entity.OnlineTime.Any() && 
                             !Status && (DateTime.Now - entity.OnlineTime.Last()).Minutes >=1
            };

            return Task.FromResult(Result);
        }
    }
}
