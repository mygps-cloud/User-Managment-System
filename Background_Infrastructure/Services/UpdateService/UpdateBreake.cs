using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Background_Infrastructure.Services.UpdateService
{
    public class UpdateBreake
    {
         public async Task UpdateWorkSchedule(IServiceScope scope, int userId, bool pingResponseStatus)
        {
            var workScheduleService = scope.ServiceProvider.GetRequiredService
            <IWorkScheduleService<WorkSchedule_ReqvestDto>>();

            var WorkSchedule = new WorkSchedule_ReqvestDto
            {
                UserId = userId,
                StartTime = pingResponseStatus ? new List<DateTime>(): new List<DateTime> { DateTime.Now },
                EndTime = pingResponseStatus ? new List<DateTime> { DateTime.Now } : new List<DateTime>()
            };

            await workScheduleService.addBreakTime(WorkSchedule, pingResponseStatus);
        }
    }
}