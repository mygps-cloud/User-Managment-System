
using ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;
namespace Ipstatuschecker.Background_Infrastructure.Services.UpdateService
{
    public class WorkScheduleService : IWorkScheduleService<WorkSchedule_ReqvestDto>
    {
        private readonly DbIpCheck _context;
        private readonly IWorkScheduleRepository _workScheduleRepository;
        private readonly IPingLogRepository _pingLogRepository;
        private readonly IServiceProvider _serviceProvider;
        private static bool busy = false;
        public WorkScheduleService(DbIpCheck context, IWorkScheduleRepository workScheduleRepository,
         IPingLogRepository pingLogRepository, IServiceProvider serviceProvider)
        {
            _context = context;
            _workScheduleRepository = workScheduleRepository;
            _pingLogRepository = pingLogRepository;
            _serviceProvider = serviceProvider;

        }
        public async Task<bool> addBreakTime(WorkSchedule_ReqvestDto entity, bool Status)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingLog = await _pingLogRepository.GetByIdAsync(entity.UserId);
            var existinworkSchedule = await _workScheduleRepository.GetBreakTimeById(entity.UserId);

            // var existingLog = await _context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);
            // var existinworkSchedule = await _context.workSchedules.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);

            var ServiceTime = _serviceProvider.GetRequiredService
            <ITimeControl<WorkSchedule_ReqvestDto, WorkScheduleResult>>();
            try
            {


                if (existinworkSchedule != null && existinworkSchedule.StartTime != null &&
                    existinworkSchedule.StartTime.Any(day => day.Day == DateTime.Now.Day) && busy && Status)
                {

                    existinworkSchedule?.EndTime?.Add(DateTime.Now);
                    busy = false;
                    return await _workScheduleRepository.Save();

                }
                else if
                (existingLog != null && existingLog.OnlineTime != null && !Status &&
                        existingLog.OnlineTime.Any(Dey => Dey.Day == DateTime.Now.Day))
                {

                    if (existinworkSchedule != null
                    && !Status && entity.StartTime?.Any(time => time.Day == DateTime.Now.Day) == true &&
                    !busy)
                    {
                        existinworkSchedule?.StartTime?.Add(DateTime.Now);
                        busy = true;
                        return await _workScheduleRepository.Save();

                    }

                    var workSchedule = new WorkSchedule
                    {
                        UserId = entity.UserId,
                        StartTime = entity.StartTime,
                        EndTime = entity.EndTime,

                    };
                    if (!busy)
                        await _workScheduleRepository.addBreakTime(workSchedule);

                    busy = true;
                    return true;
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



