
using ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Background_Infrastructure.Services.TimeControlServices
{
    public class TimeControlWorkScheduleService : ITimeControl<WorkSchedule_ReqvestDto, WorkScheduleResult>
    {
       

        public Task<WorkScheduleResult> TimeControlResult(WorkSchedule_ReqvestDto entity, bool Status)
        {
            
            var Result= new WorkScheduleResult
            {
                HasOnlineRecordForToday=entity.StartTime?.Any(time => time.Day == DateTime.Now.Day)??false,
                HasSufficientTimePassed = (entity.StartTime?.Count > 0 && (entity.StartTime?.
                Any(time => time.Day == DateTime.Now.Day) ?? false)),
                HasOfflineRecordForToday=entity.StartTime?.Any(time => time.Day == DateTime.Now.Day)?? false,
                LastTimeIn=Status


            };
             return Task.FromResult(Result);
        }
    }
}