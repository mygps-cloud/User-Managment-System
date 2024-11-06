using Abstractions.interfaces;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;



namespace Mvc.Infrastructure.Persistence
{
    public class UserCommandIRepository : ICommandUserRepository<User>
    {
        private readonly DbIpCheck _context;

        public UserCommandIRepository(DbIpCheck context)
        {
            _context = context;
        }

        public async Task<bool> Create(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> Update(User entity)
        {
            var entry = _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }
    }

}