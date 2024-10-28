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
    if (entity == null)
        throw new ArgumentNullException(nameof(entity));

    if (entity.UserId <= 0) 
        throw new ArgumentException("UserId must be a valid ID.", nameof(entity.UserId));

    try
    {
   
        var userExists = await context.Users
            .AnyAsync(u => u.Id == entity.UserId);

        if (!userExists)
        {
            throw new Exception($"User with ID {entity.UserId} does not exist.");
        }


        var pingLog = new PingLog
        {
            UserId = entity.UserId,
            OnlieTime = entity.OnlieTime,
            OflineTime = entity.OflineTime,
        };

        // Add the new log and save changes
        await context.PingLog.AddAsync(pingLog);
        await context.SaveChangesAsync();

        return true;
    }
    catch (DbUpdateException dbEx)
    {
       
        throw new Exception("Database error occurred while saving changes.", dbEx.InnerException ?? dbEx);
    }
    catch (Exception ex)
    {
       
        throw new Exception("An error occurred while processing your request.", ex);
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