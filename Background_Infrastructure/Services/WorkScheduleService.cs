

using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Background_Infrastructure.Services
{
    public  class WorkScheduleService( DbIpCheck context,
    IWorkScheduleRepository workScheduleRepository) 
    : IWorkScheduleService<WorkSchedule_ReqvestDto>
    {
        public async Task<bool> addBreakTime(WorkSchedule_ReqvestDto entity)
    {
    //==========================================================================================================//     
         
  if (entity == null) throw new ArgumentNullException(nameof(entity));

var existingLog = await context.workSchedules.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);

var hasOnlineRecordForToday = existingLog?.StartTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;

 var hasSufficientTimePassed = existingLog?.StartTime?.Count > 0 ;

var hasOfflineRecordForToday = existingLog?.EndTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;

//===============================================================================================================//

    try
    {
      
        var workSchedule = new WorkSchedule
        {
            UserId = entity.UserId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
        };

        if (existingLog != null)
        {
            
            // if (!hasOnlineRecordForToday )
            // {
            //     existingLog?.StartTime?.Add(DateTime.Now);
            // }

            if (!hasOfflineRecordForToday &&
                entity?.EndTime?.Count>0)
            {
                existingLog?.EndTime?.Add(DateTime.Now);
             
            }

            return await workScheduleRepository.Save();
        }
        else
        {
          
            if (entity.StartTime?.Count > 0)
            {
                await workScheduleRepository.addBreakTime(workSchedule);
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

        public Task<WorkSchedule_ReqvestDto> GetBreakTime()
        {
            throw new NotImplementedException();
        }
    }
}