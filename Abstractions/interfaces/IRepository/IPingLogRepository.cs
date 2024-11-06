
using Ipstatuschecker.DomainEntity;


namespace Ipstatuschecker.Abstractions.interfaces.IRepository
{
    public interface IPingLogRepository 
    {
        Task<bool> Save();
        Task<bool> Create(PingLog entity);
         Task<List<PingLog>> GetAll();
         Task<PingLog> GetByIdAsync(int id);
         Task<PingLog> GetByNameAsync(string name);


    }
}