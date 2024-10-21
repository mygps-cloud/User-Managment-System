using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Interfaces;

namespace Ipstatuschecker.Services
{
    public class ServiceIpStatus(IQueryIpStatusRepository<User> qeuryIpStatusRepository,
    ICommandIpStatusRepository<User> commandIpStatusRepository) : Iservices<User>
    {
        public Task<User> AddNewUser(User entety)
        {
            throw new NotImplementedException();
        }

        public Task<User> DelteUserById(User entety)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUserNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateNewUser(User entety)
        {
            throw new NotImplementedException();
        }
    }
}