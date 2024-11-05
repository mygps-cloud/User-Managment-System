using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Abstractions.interfaces.IRepository
{
    public interface IWorkScheduleRepository
    {
         Task<bool> addBreakTime(WorkSchedule workSchedule);
         Task<List<WorkSchedule>> GetAllBreakTime();
         Task<WorkSchedule> GetBreakTimeById(int id);
         Task<bool>Save();
    }
}