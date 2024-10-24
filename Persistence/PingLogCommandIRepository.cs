using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.interfaces;

namespace Ipstatuschecker.Persistence
{
    public class PingLogCommandIRepository (DbIpCheck context): ICommandIpStatusRepository<PingLog>
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

        public Task<PingLog> UpdateUser(PingLog entety)
        {
            throw new NotImplementedException();
        }
    }
}