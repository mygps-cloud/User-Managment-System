using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IWorkScheduleService
    {
         Task addBreakTime(WorkSchedule_ReqvestDto  workSchedule_ReqvestDto );
         Task<WorkSchedule_ResponseDto> GetBreakTime();

         
    }
}