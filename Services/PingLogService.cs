using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Persistence;

namespace Ipstatuschecker.Services
{
    public class PingLogService(DbIpCheck _context) : Iservices<PingLogDtoReqvest>
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

        public Task<List<PingLogDtoReqvest>> GetAllUsers()
        {
            throw new NotImplementedException();
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
    }
}