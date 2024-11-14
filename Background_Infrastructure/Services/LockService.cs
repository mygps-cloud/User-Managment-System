using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Dto;


public  class LockService
{
    private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
    private readonly IPingLogRepository _pingLogRepository;
    private readonly ILogger<LockService> _logger;

    public LockService(IPingLogRepository pingLogRepository, ILogger<LockService> logger)
    {
        _pingLogRepository = pingLogRepository;
        _logger = logger;
    }

    public async Task<PingLogDtoResponse?> GetWithLockAsync(int entityUserId, bool isWriteOperation = false)
    {
        _logger.LogInformation("Attempting to acquire {LockType} lock for UserId: {UserId}",
            isWriteOperation ? "write" : "read", entityUserId);

        bool lockAcquired = EnterLock(isWriteOperation);

        try
        {
            if (!lockAcquired)
            {
                _logger.LogWarning("Unable to acquire the {LockType} lock for UserId: {UserId}",
                    isWriteOperation ? "write" : "read", entityUserId);
                throw new InvalidOperationException("Unable to acquire the required lock.");
            }

            _logger.LogInformation("Lock acquired successfully for UserId: {UserId}", entityUserId);

            var existingLog = await _pingLogRepository.GetByIdAsync(entityUserId);

            if (existingLog == null)
            {
                _logger.LogWarning("No PingLog found for UserId: {UserId}", entityUserId);
                return null;
            }

            _logger.LogInformation("PingLog found for UserId: {UserId}", entityUserId);

            var response = new PingLogDtoResponse
            {
                UserId = existingLog.UserId,
                OnlineTime = existingLog.OnlineTime,
                OflineTime = existingLog.OflineTime,
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving PingLog for UserId: {UserId}", entityUserId);
            throw;
        }
        finally
        {
            ExitLock(isWriteOperation, lockAcquired);
            _logger.LogInformation("Lock released for UserId: {UserId}", entityUserId);
        }
    }

    private static bool EnterLock(bool isWriteOperation)
    {
        if (isWriteOperation)
        {
            return _lock.TryEnterWriteLock(1000);
        }
        else
        {
            return _lock.TryEnterReadLock(1000);
        }
    }

    private static void ExitLock(bool isWriteOperation, bool lockAcquired)
    {
        if (!lockAcquired) return;

        if (isWriteOperation && _lock.IsWriteLockHeld)
        {
            _lock.ExitWriteLock();
        }
        else if (_lock.IsReadLockHeld)
        {
            _lock.ExitReadLock();
        }
    }
}
