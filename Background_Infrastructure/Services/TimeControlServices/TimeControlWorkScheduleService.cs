
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
                HasOfflineRecordForToday=entity.StartTime?.Any(time => time.Day == DateTime.Now.Day)?? false,
                LastTimeIn = Status&& !(entity.EndTime?.Any(time => time.Day == DateTime.Now.Day) ?? false)


            };
             return Task.FromResult(Result);
        }
    }
}