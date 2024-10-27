using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Persistence;

namespace Ipstatuschecker.Services
{
    public class PingLogService(DbIpCheck _context,PingLogCommandIRepository pingLogCommandIRepository) : Iservices<PingLogDtoReqvest>
    {
   
public async Task<bool> AddNewUser(PingLogDtoReqvest entity)
{
    
try
    {
        var pingLog = new PingLog
        {
            OnlieTime = entity.OnlieTime,
            OflineTime = entity.OflineTime,
        };

        await _context.PingLog.AddAsync(pingLog);
        await _context.SaveChangesAsync();
        return true;
    }
    catch (Exception ex)
    {
        throw new Exception("data base erorsss ->>>", ex);
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
            OflineTime = log.OflineTime, 
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