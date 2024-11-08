using Abstractions.interfaces.IRepository;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;


namespace Mvc.Infrastructure.Persistence
{
    public class UserQueryRepository(DbIpCheck context) : IQueryUserRepository<User>
    {
          public async Task<List<User>> GetAll()
        {
            return await context.Users.
            Include(param=>param.Devices).
            Include(param=>param.IpStatuses).
            Include(param=>param.workSchedule).
            Include(param=>param.PingLog)
            
            .AsNoTracking().ToListAsync()
            ??
             throw new Exception("User is empty");
        }





        // public async Task<List<User>> GetByIdAsync(int id)
        // {
        //     var users = await context.Users
        //         .Include(param => param.Devices)
        //         .Include(param => param.IpStatuses)
        //         .Include(param => param.workSchedule)
        //         .Include(param => param.PingLog)
        //         .AsNoTracking()
        //         .Where(userbyid => userbyid.Id == id)
        //         .ToListAsync();

        //     if (users == null || users.Count == 0)
        //     {
        //         throw new Exception("User Id not found");
        //     }

        //     return users;
        // }

        public async Task<User> GetByIdAsync(int Id)
        {
            return await context.Users.
            Include(param=>param.Devices).
            Include(param=>param.IpStatuses).
            Include(param=>param.workSchedule).
            Include(param=>param.PingLog).
            AsNoTracking().FirstOrDefaultAsync(userbyid=>userbyid.Id==Id)??
            throw new Exception("User Id not found");
        }

        public async Task<User> GetByNameAsync(string name)
        {
           
            return await context.Users.
            Include(param=>param.Devices).
            Include(param=>param.workSchedule).
            Include(param=>param.IpStatuses).
            Include(param=>param.PingLog).
            AsNoTracking().FirstOrDefaultAsync(x => x.Name == name)
            ?? throw new Exception("User Name not found");
        }
    }
}