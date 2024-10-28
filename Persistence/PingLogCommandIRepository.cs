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
              context.Add(entety);
              await context.SaveChangesAsync();
           return true;
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

        public Task<PingLog> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PingLog> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<PingLog> UpdateUser(PingLog entety)
        {
            throw new NotImplementedException();
        }


    }
}