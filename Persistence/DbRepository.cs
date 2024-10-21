using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Ipstatuschecker.Persistence
{
    public class DbRepository(IpCheck context) : ICommandIpStatusRepository<User>, IQueryIpStatusRepository<User>
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



//=====================================================================//
        public async Task<List<User>> GetAll()
        {
            return await context.Users.AsNoTracking().ToListAsync()??
             throw new Exception("User is empty");
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(X=>id==id)??
            throw new Exception("User Id not found");
        }

        public async Task<User> GetByNameAsync(string name)
        {
           
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name)
            ?? throw new Exception("User Name not found");
        }

       
    }
}