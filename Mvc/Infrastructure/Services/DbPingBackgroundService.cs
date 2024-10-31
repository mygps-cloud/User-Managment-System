
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Dto;



namespace Ipstatuschecker.Mvc.Infrastructure.Services
{
    public class DbPingBackgroundService : IUserservices<IpStatusDto>
    {
        private readonly IPstatusIQueryPingDbRepository pstatusIQueryPingDbRepository;

        public DbPingBackgroundService(IPstatusIQueryPingDbRepository pstatusIQueryPingDbRepository)
        {
            this.pstatusIQueryPingDbRepository = pstatusIQueryPingDbRepository;
        }

        public async Task<bool> AddNewUser(IpStatusDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DelteUserById(int entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IpStatusDto>> GetAllUsers()
        {
            var all_Ip = await pstatusIQueryPingDbRepository.GetAll();
             
            var UsersIp = all_Ip.Select(Ip => new IpStatusDto
            {
                Id = Ip.Id,
                IpAddress = Ip.IpAddress,
                Status = Ip.Status
            }).ToList();


            return UsersIp;
        }

        public async Task<IpStatusDto> GetByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IpStatusDto> GetByUserNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IpStatusDto> UpdateNewUser(IpStatusDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
