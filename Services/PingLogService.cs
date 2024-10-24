using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Persistence;

namespace Ipstatuschecker.Services
{
    public class PingLogService(PingLogCommandIRepository pingLogCommandIRepository) : Iservices<PingLogDtoReqvest>
    {
     public async Task<bool> AddNewUser(PingLogDtoReqvest entety)
{
    var Pinglog = new PingLog
    {
        Id = entety.Id,
        OnlieTime = entety.OnlieTime,
        OflineTime = entety.OflineTime
    };
    
    await pingLogCommandIRepository.CreateUser(Pinglog);
    return true;
}


        public Task<bool> DelteUserById(int entetyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PingLogDtoReqvest>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<PingLogDtoReqvest> GetByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PingLogDtoReqvest> GetByUserNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<PingLogDtoReqvest> UpdateNewUser(PingLogDtoReqvest entety)
        {
            throw new NotImplementedException();
        }
    }
}