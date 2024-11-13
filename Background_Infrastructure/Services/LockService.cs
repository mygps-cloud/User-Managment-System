
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Dto;

public static class LockService<TEntity, TRepository>
    where TEntity : class, IUserEntity
    where TRepository : IPingLogRepository
{
    private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

    public async static Task<PingLogDtoResponse?> GetWithLockAsync(TEntity entity, TRepository pingLogRepository, bool isWriteOperation = false)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        if (pingLogRepository == null) throw new ArgumentNullException(nameof(pingLogRepository));

        
        if (isWriteOperation)
        {
            _lock.EnterWriteLock();
        }
        else
        {
            _lock.EnterReadLock();
        }

        try
        {
#pragma warning disable CS8629
            var existingLog = await pingLogRepository.GetByIdAsync(entity.UserId.Value);
#pragma warning restore CS8629

            if (existingLog == null)
            {
                return null;
            }

            var response = new PingLogDtoResponse
            {
                UserId = existingLog.UserId,
                OnlineTime = existingLog.OnlineTime,
                OflineTime = existingLog.OflineTime,
            };

            return response;
        }
        finally
        {
            
            if (isWriteOperation)
            {
                _lock.ExitWriteLock();
            }
            else
            {
                _lock.ExitReadLock();
            }
        }
    }
}
