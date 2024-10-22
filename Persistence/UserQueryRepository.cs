using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Persistence
{
    public class UserQueryRepository(IpCheck context) : IQueryIpStatusRepository<User>
    {
          public async Task<List<User>> GetAll()
        {
            return await context.Users.
            Include(param=>param.Devices).
            Include(param=>param.IpStatuses)
            .AsNoTracking().ToListAsync()
            ??
             throw new Exception("User is empty");
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users.
            Include(param=>param.Devices).
            Include(param=>param.IpStatuses).
            AsNoTracking().FirstOrDefaultAsync(X=>id==id)??
            throw new Exception("User Id not found");
        }

        public async Task<User> GetByNameAsync(string name)
        {
           
            return await context.Users.
            Include(param=>param.Devices).
            Include(param=>param.IpStatuses).
            AsNoTracking().FirstOrDefaultAsync(x => x.Name == name)
            ?? throw new Exception("User Name not found");
        }
    }
}