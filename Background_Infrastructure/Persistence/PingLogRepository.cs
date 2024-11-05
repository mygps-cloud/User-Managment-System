
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Background_Infrastructure.Persitence
{
    public class PingLogRepository(DbIpCheck context) : IPingLogRepository
    {
       public async Task<bool> Create(PingLog entity)
        {
            context.Add(entity);
            return await context.SaveChangesAsync() > 0 ? true : false;
        }


        public async Task<bool> Delete(int id)
        {
            var existingUser = await context.PingLog.FindAsync(id);
            if (existingUser == null) throw new Exception("User not found."); 
            context.PingLog.Remove(existingUser);
            return await context.SaveChangesAsync()>0?true:false;
        }

        public async Task<List<PingLog>> GetAll()
        {
           return await context.PingLog.
            Include(user=>user.User)
            .AsNoTracking()
            .ToListAsync() ??
             throw new Exception("User is empty");
        }

      

        public async Task<PingLog> GetByIdAsync(int id)
        {
          
           var user= await context.PingLog.
             AsNoTracking()
            .FirstOrDefaultAsync(param=>param.UserId==id);
            // ?? throw new Exception("User is empty");
              return user;
        }

        public async Task<PingLog> GetByNameAsync(string name)
        {

            throw new NotImplementedException();
        }

        public async Task<bool> Save()
        {
           return await context.SaveChangesAsync()>0?true:false;
        }

        public Task<PingLog> Update(PingLog entety)
        {
            throw new NotImplementedException();
        }
    }
}