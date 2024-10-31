using Abstractions.interfaces;
using Abstractions.interfaces.IRepository;
using Ipstatuschecker.DomainEntity;


namespace Ipstatuschecker.Abstractions.interfaces.IRepository
{
    public interface IPingLogRepository:ICommandIpStatusRepository<PingLog>,
    IQueryIpStatusRepository<PingLog>
    {
        Task<bool>Save();
      
        
    }
}