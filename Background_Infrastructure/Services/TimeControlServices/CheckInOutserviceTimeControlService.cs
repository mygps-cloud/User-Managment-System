using ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Background_Infrastructure.Services.TimeControlServices
{
    public class CheckInOutserviceTimeControlService : ITimeControl<PingLogDtoReqvest,CheckInOutServiceResult>
    {
     
        public Task<CheckInOutServiceResult> TimeControlResult(PingLogDtoReqvest entity, bool Status)
        {
            
            var Result= new CheckInOutServiceResult
            {
                HasOnlineRecordForToday=entity.OnlieTime?.Any(time => time.Day == DateTime.Now.Day)??false,
                HasSufficientTimePassed = (entity.OnlieTime?.Count > 0 && (entity.OnlieTime?.
                Any(time => time.Day == DateTime.Now.Day) ?? false)),
                HasOfflineRecordForToday=entity.OflineTime?.Any(time => time.Day == DateTime.Now.Day)?? false,
                LastTimeIn =  (DateTime.Now - entity.OnlieTime.Last()).Minutes >= 2

            };
             return Task.FromResult(Result);
        }
    }
}