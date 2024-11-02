using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IWorkScheduleService<T> where T : class
    {
         Task<bool> addBreakTime(T breakTime);
         Task<T> GetBreakTime();
         Task<bool> Save();

         
    }
}