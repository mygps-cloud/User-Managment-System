using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.interfaces;

namespace Ipstatuschecker.Persistence
{
    public class UserCommandIRepository(DbIpCheck context) : ICommandIpStatusRepository<User>
    {
        public async Task<bool> CreateUser(User entity)
        {
            var entry = await context.Users.AddAsync(entity); 
            await context.SaveChangesAsync(); 
            return true; 
        }

  public async Task<bool> DelteUser(int id)
        {
            var existingUser = await context.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new Exception("User not found."); 
            }

            context.Users.Remove(existingUser);
            await context.SaveChangesAsync();
            return true;
        }

    

          public async Task<User> UpdateUser(User entity)
            {
                var entry = context.Users.Update(entity); 
                 await context.SaveChangesAsync(); 
                return entry.Entity; 
            }
    }
}