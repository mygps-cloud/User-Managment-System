using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Dto;

public static class SemaphoreService<TEntity, TRepository> where TEntity : class, IUserEntity where TRepository : IPingLogRepository
{
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public async static Task<PingLogDtoResponse?> GetWithLockAsync(TEntity entity, TRepository pingLogRepository)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        if (pingLogRepository == null) throw new ArgumentNullException(nameof(pingLogRepository));

        await _semaphore.WaitAsync();
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
                OflineTime = existingLog.OflineTime
            };

            return response;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
