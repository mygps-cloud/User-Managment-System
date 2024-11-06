
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;



namespace Ipstatuschecker.Background_Infrastructure.Persitence

{
public class StatusIpRepository :IPstatusRepository
{
    private readonly DbIpCheck _context;

    public StatusIpRepository(DbIpCheck context)
    {
        _context = context;
    }

        public async Task<List<IpStatus>> GetallIpStatus()
        =>await _context.IpStatuses.ToListAsync(); 
       

}


}