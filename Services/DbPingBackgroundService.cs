

using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Interfaces;

namespace ipstatuschecker.Services
{
    public class DbPingBackgroundService (IQueryIpStatusRepository<IpStatus> queryIpStatusRepository): Iservices<IpStatusDto>
    {
        public Task<bool> AddNewUser(IpStatusDto entety)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DelteUserById(int entetyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<IpStatusDto>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IpStatusDto> GetByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IpStatusDto> GetByUserNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IpStatusDto> UpdateNewUser(IpStatusDto entety)
        {
            throw new NotImplementedException();
        }
    }
}