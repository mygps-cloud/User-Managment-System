
using Abstractions.interfaces;
using Ipstatuschecker.Abstractions.interfaces;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;
using Mvc.Infrastructure.Persistence;

namespace Background_Infrastructure.Services
{
    public class PingLogService( DbIpCheck context,
    PingLogCommandIRepository pingLogCommandIRepository,IPingLogRepository pingLogRepository) 
    : Iservices<PingLogDtoReqvest>
    {
  
public async Task<bool> AddNewUser(PingLogDtoReqvest entity)
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
    
//   var existingLog= await pingLogRepository.GetByIdAsync(entity.UserId);

    var existingLog= await context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);
     if(existingLog!=null)
     {
       
            var timeToAdd = existingLog?.OflineTime; 
            var hasOnlineRecordForToday = existingLog?.OnlieTime?
            .Any(time => time.Day == DateTime.Now.Day) ?? false;
            var hasOflineRecordForToday = existingLog?.OflineTime?
            .Any(time => time.Day == DateTime.Now.Day) ?? false;

            if (!hasOnlineRecordForToday && entity?.OnlieTime?.Count > 0)
            existingLog?.OnlieTime?.Add(DateTime.Now);
            else if(entity?.OflineTime != null&&!hasOflineRecordForToday) 
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





        public Task<bool> DelteUserById(int entetyId)
        {
            throw new NotImplementedException();
        }

 public async Task<List<PingLogDtoResponse>> GetAllUsers2()
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



        public Task<PingLogDtoReqvest> GetByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PingLogDtoReqvest> GetByUserNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<PingLogDtoReqvest> UpdateNewUser(PingLogDtoReqvest entety)
        {
            throw new NotImplementedException();
        }

        Task<List<PingLogDtoReqvest>> Iservices<PingLogDtoReqvest>.GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}