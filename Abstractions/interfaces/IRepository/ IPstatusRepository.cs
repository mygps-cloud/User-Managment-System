using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Abstractions.interfaces.IRepository
{
    public interface  IPstatusRepository
    {
        Task<List<IpStatus>> GetallIpStatus();
         
    }
}