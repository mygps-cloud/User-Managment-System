
using Abstractions.interfaces.IRepository;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;



namespace Ipstatuschecker.Background_Infrastructure.Persitence

{
public class IPstatusIQueryPingDbRepository : IQueryIpStatusRepository<IpStatus>
{
    private readonly DbIpCheck _context;

    public IPstatusIQueryPingDbRepository(DbIpCheck context)
    {
        _context = context;
    }

    public async Task<List<IpStatus>> GetAll()
    {
        var ipStatuses = await _context.IpStatuses
            .AsNoTracking()
            .ToListAsync();

        if (ipStatuses == null || !ipStatuses.Any())
        {
            throw new Exception("No IP statuses found.");
        }

        return ipStatuses;
    }

    public async Task<IpStatus> GetByIdAsync(int id)
    {
        var ipStatus = await _context.IpStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (ipStatus == null)
        {
            throw new Exception("IP status not found.");
        }

        return ipStatus;
    }

    public async Task<IpStatus> GetByNameAsync(string name)
    {
        var ipStatus = await _context.IpStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IpAddress == name);

        if (ipStatus == null)
        {
            throw new Exception("IP status not found.");
        }

        return ipStatus;
    }
}


}