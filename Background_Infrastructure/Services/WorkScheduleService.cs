

using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Background_Infrastructure.Services
{
    public  class WorkScheduleService( DbIpCheck context,
    PingLogCommandIRepository pingLogCommandIRepository,
    IPingLogRepository pingLogRepository) : IWorkScheduleService<PingLogDtoReqvest>
    {
        public async Task<bool> addBreakTime(PingLogDtoReqvest entity)
        {
           if (entity == null) throw new ArgumentNullException(nameof(entity));

var existingLog = await context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);
var hasOnlineRecordForToday = existingLog?.OnlieTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;
var hasSufficientTimePassed = existingLog?.OnlieTime?.Count > 0 &&(DateTime.Now - existingLog.OnlieTime.Last()).
Minutes >= 1;
var hasOfflineRecordForToday = existingLog?.OflineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;

    try
    {
      
        var pingLog = new PingLog
        {
            UserId = entity.UserId,
            OnlieTime = entity.OnlieTime,
            OflineTime = entity.OflineTime,
        };

        if (existingLog != null)
        {
            
            if (!hasOnlineRecordForToday && hasSufficientTimePassed)
            {
                existingLog?.OnlieTime?.Add(DateTime.Now);
            }

            if (existingLog?.OnlieTime?.Count > 0 && !hasOfflineRecordForToday &&
                (DateTime.Now - existingLog.OnlieTime.Last()).Minutes >= 1
                &&entity?.OflineTime?.Count>0)
            {
                existingLog?.OflineTime?.Add(DateTime.Now.AddMinutes(-1));
             
            }

            return await pingLogRepository.Save();
        }
        else
        {
          
            if (entity.OnlieTime?.Count > 0)
            {
                await pingLogRepository.Create(pingLog);
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

        public Task<PingLogDtoReqvest> GetBreakTime()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}