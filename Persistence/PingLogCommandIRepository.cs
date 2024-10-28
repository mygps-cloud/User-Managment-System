using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Persistence
{
    public class PingLogCommandIRepository (DbIpCheck context): ICommandIpStatusRepository<PingLog>,IQueryIpStatusRepository<PingLog>
    {

        public async Task<bool> CreateUser(PingLog entety)
        {
             try
    {
        
        var userExists = await context.Users.FirstOrDefaultAsync(u => u.Id == entety.UserId);
      if(userExists == null)    
      {

        throw   new Exception($"user id -------------------------------- {userExists.Id}");    
      }
        var pingLog = new PingLog
        {
            OnlieTime = entety.OnlieTime,
            OflineTime = entety.OflineTime,
            UserId = entety.UserId
        };

        await context.PingLog.AddAsync(pingLog);
        await context.SaveChangesAsync();
        return true;
    }
    catch (Exception ex)
    {
        throw new Exception("Database error ->>>", ex);
    }
        }

        public Task<bool> DelteUser(int entetyId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PingLog>> GetAll()
        {
             var OflineUsers=await context.PingLog.
           Include(p=>p.User)
          .AsNoTracking().
          ToListAsync();
           return OflineUsers;
        }

        public  async Task<PingLog> GetByIdAsync(int id)
        {
            var GetById= await context.PingLog. 
            Include(p=>p.User)
          .AsNoTracking().
          FirstOrDefaultAsync(param=>param.Id==id);
           return GetById;
        }

        public async Task<PingLog> GetByNameAsync(string name)
        {
             var GetByName= await context.PingLog. 
            Include(p=>p.User)
          .AsNoTracking().
          FirstOrDefaultAsync(param=>param.User.Name==name);
           return GetByName;
        }

        public Task<PingLog> UpdateUser(PingLog entety)
        {
            throw new NotImplementedException();
        }


    }
}