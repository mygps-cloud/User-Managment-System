using ipstatuschecker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ipstatuschecker.DbContextSql
{
    public class IpCheck : Microsoft.EntityFrameworkCore.DbContext
{
    public IpCheck(DbContextOptions<IpCheck> options) : base(options)
    {
    }

    public DbSet<IpStatus> IpStatuses { get; set; }
}

    
}