
using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Interfaces;
using Microsoft.EntityFrameworkCore;

public class DbRepositoryIPstatusPing : IQueryIpStatusRepository<IpStatus>
{
    private readonly IpCheck _context;

    public DbRepositoryIPstatusPing(IpCheck context)
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
