using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Services
{
    public class PingLogService(DbIpCheck context,PingLogCommandIRepository pingLogCommandIRepository) : Iservices<PingLogDtoReqvest>
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
     var existingLog = await context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);
     if(existingLog!=null)
     {
       var timeToAdd = entity?.OnlieTime?.Count > 0 ? existingLog?.OnlieTime : existingLog?.OflineTime;
       timeToAdd?.Add(DateTime.UtcNow);
        await context.SaveChangesAsync();
        return true; 
     } else{
        await context.PingLog.AddAsync(pingLog);
        await context.SaveChangesAsync();
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
            OflineTime = log.OflineTime.ToList(), 
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