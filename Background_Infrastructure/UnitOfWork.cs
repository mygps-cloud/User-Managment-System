
using Abstractions.interfaces;
using Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;

namespace Ipstatuschecker.Background_Infrastructure
{


public class UnitOfWork : IUnitOfWork
{
    private readonly DbIpCheck _context;
    private ICommandIpStatusRepository<PingLog>? _pingLogCommandRepository;
    private IQueryIpStatusRepository<PingLog>? _pingLogQueryRepository;



    public UnitOfWork(DbIpCheck context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public ICommandIpStatusRepository<PingLog> PingLogCommandRepository =>
        _pingLogCommandRepository ??= new PingLogRepository(_context);

    public IQueryIpStatusRepository<PingLog> PingLogQueryRepository =>
        _pingLogQueryRepository ??= new PingLogRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}



}