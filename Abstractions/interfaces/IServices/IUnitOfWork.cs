
using Abstractions.interfaces;
using Abstractions.interfaces.IRepository;
using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
   public interface IUnitOfWork : IDisposable
{
    ICommandIpStatusRepository<PingLog> PingLogCommandRepository { get; }
    IQueryIpStatusRepository<PingLog> PingLogQueryRepository { get; }
    Task<int> SaveChangesAsync();

    
}

}