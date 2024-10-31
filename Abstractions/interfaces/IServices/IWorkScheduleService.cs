using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IWorkScheduleService
    {
        public Task addBreakTime(WorkSchedule_ReqvestDto  workSchedule_ReqvestDto );
        public Task<WorkSchedule_ResponseDto> GetBreakTime();

         
    }
}