using Abstractions.interfaces;
using Ipstatuschecker.DomainEntity;


namespace Ipstatuschecker.Abstractions.interfaces
{
    public interface IPingLogRepository:ICommandIpStatusRepository<PingLog>,
    IQueryIpStatusRepository<PingLog>
    {
        Task<bool>Save();
      
        
    }
}