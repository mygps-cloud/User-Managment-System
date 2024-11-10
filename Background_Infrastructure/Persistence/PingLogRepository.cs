
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


//==========================================================================================//

        public async Task<List<PingLog>> GetAll()
        => await context.PingLog.Include(user=>user.User)
        .ToListAsync() ??throw new Exception
        ("PingLogRepository->>>>>>>>>>User is empty");

        public async Task<PingLog> GetByIdAsync(int id)
        =>id>0?await context.PingLog.Include(user=>user.User)
        .FirstOrDefaultAsync(param=>param.UserId==id)
        ??throw new Exception
        ("Message PingLogRepository->>>>>User is empty")
        :throw new Exception
        ("Message PingLogRepository->>>>> not found user ID.");

        public async Task<PingLog> GetByNameAsync(string name)
        {

            throw new NotImplementedException();
        }

        public async Task<bool> Save()
        => await context.SaveChangesAsync()>0?true:false;
        


    }
}