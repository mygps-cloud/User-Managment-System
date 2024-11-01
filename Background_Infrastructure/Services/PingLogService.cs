
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Background_Infrastructure.Services;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;


namespace Background_Infrastructure.Services
{
    public class PingLogService( DbIpCheck context,
    PingLogCommandIRepository pingLogCommandIRepository,
    IPingLogRepository pingLogRepository,TimeControlService timeControlService) 
    : IPingLogService
    {
  

public async Task<bool> addService(PingLogDtoReqvest entity)
       
{
    if (entity == null)throw new ArgumentNullException(nameof(entity));

        try
        {
            var pingLog = new PingLog
            {
                UserId = entity.UserId,
                OnlieTime = entity.OnlieTime,
                OflineTime = entity.OflineTime,
            };



  
    var existingLog= await context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);
    var BreakTime= await context.workSchedules.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);
     if(existingLog!=null)
     {
            var addBreakTime=BreakTime?.StartTime;
            var timeToAdd = existingLog?.OflineTime; 
            var hasOnlineRecordForToday = existingLog?.OnlieTime?
            .Any(time => time.Day == DateTime.Now.Day) ?? false;
            var hasOflineRecordForToday = existingLog?.OflineTime?
            .Any(time => time.Day == DateTime.Now.Day) ?? false;


            if (!hasOnlineRecordForToday && entity?.OnlieTime?.Count > 0)
            existingLog?.OnlieTime?.Add(DateTime.Now);
            else if(entity?.OflineTime != null&&!hasOflineRecordForToday
            &&timeControlService.IsWithinTimeFrame==false) 
            timeToAdd?.Add(DateTime.Now);
           
            return  await pingLogRepository.Save();

     }
      else
     {
       await pingLogRepository.Create(pingLog);
       return true;
    }

            
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database error occurred while saving changes.", dbEx.InnerException ?? dbEx);
        }
        
    }

        public async Task<List<PingLogDtoResponse>> GetAll()
        {
            {
            var offlineAllUsers = await pingLogCommandIRepository.GetAll();

            var pingLogDtoRequests = offlineAllUsers
                .Where(log => log.OnlieTime != null && log.OflineTime.Any())
                .Select(log => new PingLogDtoResponse
                {
                    Id = log.Id,
                    OnlieTime = log.OnlieTime, 
                    OflineTime = log.OflineTime?.ToList(), 
                    _UserDto = log.User != null ? new UserDto
                    {
                        Id = log.User.Id,
                        Name = log.User.Name 
                    } : null 
                })
                .ToList();

            return pingLogDtoRequests;
}
        }

     
    }
}