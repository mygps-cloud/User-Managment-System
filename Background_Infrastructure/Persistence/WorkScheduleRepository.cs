using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Background_Infrastructure.Persistence
{
    public class WorkScheduleRepository(DbIpCheck context) : IWorkScheduleRepository
    {
        public async Task<bool> addBreakTime(WorkSchedule entety)
        {
            context.workSchedules.Add(entety);
            return await Save();
        }

        public async Task<List<WorkSchedule>> GetAllBreakTime()
        =>await context.workSchedules.AsNoTracking().ToListAsync();
        public async Task<WorkSchedule> GetBreakTimeById(int id)
        => id > 0? (await context.workSchedules.FirstOrDefaultAsync(param => param.UserId == id)) 
        ?? new WorkSchedule()
        : new WorkSchedule();


        public async Task<bool> Save()=> await context.SaveChangesAsync() > 0 ? true : false;
    }
}