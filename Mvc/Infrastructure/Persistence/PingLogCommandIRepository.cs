using Abstractions.interfaces;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;


namespace Mvc.Infrastructure.Persistence
{
    public class PingLogCommandIRepository (DbIpCheck context): ICommandIpStatusRepository<PingLog>,IQueryIpStatusRepository<PingLog>
    {
public async Task<bool> AddNewUser(PingLogDtoReqvest entity)
{
    if (entity == null)
        throw new ArgumentNullException(nameof(entity));

    if (entity.UserId <= 0) 
        throw new ArgumentException("UserId must be a valid ID.", nameof(entity.UserId));

    try
    {
        var userExists = await context.Users.FindAsync(entity.UserId);
        if (userExists == null)
        {
            throw new Exception($"User with ID {entity.UserId} does not exist.");
        }

        var pingLog = new PingLog
        {
            OnlieTime = entity.OnlieTime,
            OflineTime = entity.OflineTime,
            UserId = entity.UserId,
            
        };

        await context.PingLog.AddAsync(pingLog);
        await context.SaveChangesAsync();
        return true;
    }
    catch (DbUpdateException dbEx)
    {
        
        throw new Exception("Database error ->>>", dbEx.InnerException);
    }
    catch (Exception ex)
    {
       
        throw new Exception("Database error ->>>", ex);
    }
}


        public Task<bool> CreateUser(PingLog entety)
        {
            throw new NotImplementedException();
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