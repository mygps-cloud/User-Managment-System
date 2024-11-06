using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IWorkScheduleService<T> where T : class
    {
         Task<bool> addBreakTime(T breakTime,bool Status);
    
         
    }
}