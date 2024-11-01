using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IWorkScheduleService
    {
         Task<bool> addBreakTime(WorkSchedule_ReqvestDto  workSchedule_ReqvestDto );
         Task<WorkSchedule_ResponseDto> GetBreakTime();

         Task<bool> Save();

         
    }
}