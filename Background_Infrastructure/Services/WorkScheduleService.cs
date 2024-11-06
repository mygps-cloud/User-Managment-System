
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Background_Infrastructure.Services
{
  public class WorkScheduleService : IWorkScheduleService<WorkSchedule_ReqvestDto>
{
    private readonly DbIpCheck _context;
    private readonly IWorkScheduleRepository _workScheduleRepository;
    private readonly IPingLogRepository _pingLogRepository;

    public WorkScheduleService(DbIpCheck context, IWorkScheduleRepository workScheduleRepository, IPingLogRepository pingLogRepository)
    {
        _context = context;
        _workScheduleRepository = workScheduleRepository;
        _pingLogRepository = pingLogRepository;
    }

    public async Task<bool> addBreakTime(WorkSchedule_ReqvestDto entity, bool Status)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingLog = await _context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);
         var existinworkSchedule = await _context.workSchedules.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);

      
        // var existingLog = await _pingLogRepository.GetByIdAsync(entity.UserId);
        //  var existinworkSchedule = await _workScheduleRepository.GetBreakTimeById(entity.UserId);

        var hasOnlineRecordForTodayCheckIn = existingLog?.OnlieTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;
        var hasSufficientTimePassed = existinworkSchedule?.StartTime?.Count > 0;
        var hasOfflineRecordForToday = existinworkSchedule?.EndTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;

        try
        {
            if (existinworkSchedule != null)
            {
                if (!hasOfflineRecordForToday && Status)
                {
                    existinworkSchedule?.EndTime?.Add(DateTime.Now);
                }

                return await _workScheduleRepository.Save();
            }
            else
            {
                if (hasOnlineRecordForTodayCheckIn && !Status)
                {
                    var workSchedule = new WorkSchedule
                    {
                        UserId = entity.UserId,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime,
                    };

                    await _workScheduleRepository.addBreakTime(workSchedule);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database error occurred while saving changes.", ex.InnerException ?? ex);
        }

        return false;
    }
}

}