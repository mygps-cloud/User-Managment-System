using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;

namespace Background_Infrastructure.Services
{
    public class CheckInOutservice : IPingLogService
    {
        private readonly DbIpCheck _context;
        private readonly IPingLogRepository _pingLogRepository;
        private readonly ILogger<CheckInOutservice> _logger;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public CheckInOutservice(DbIpCheck context, IPingLogRepository pingLogRepository, ILogger<CheckInOutservice> logger)
        {
            _context = context;
            _pingLogRepository = pingLogRepository;
            _logger = logger;
        }

        public async Task<bool> addTimeInService(PingLogDtoReqvest entity, bool Status)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

         
                  var existingLog = await _context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);
            
                // var existingLog = await _pingLogRepository.GetByIdAsync(entity.UserId);

                var hasOnlineRecordForToday = HasOnlineRecordForToday(existingLog);
                var hasSufficientTimePassed = HasSufficientTimePassed(existingLog);
                var hasOfflineRecordForToday = HasOfflineRecordForToday(existingLog);

                try
                {
                    if (existingLog != null)
                    {
                        if (!hasOnlineRecordForToday && Status)
                        {
                            existingLog?.OnlieTime?.Add(DateTime.Now);
                        }

                        if (existingLog?.OnlieTime?.Count > 0 && !hasOfflineRecordForToday &&
                            (DateTime.Now - existingLog.OnlieTime.Last()).Minutes >= 20
                            && entity?.OflineTime?.Count > 0)
                        {
                            existingLog?.OflineTime?.Add(DateTime.Now.AddMinutes(-20));
                        }

                        return await _pingLogRepository.Save();
                    }
                    else
                    {
                        var pingLog = new PingLog
                        {
                            UserId = entity.UserId,
                            OnlieTime = entity.OnlieTime,
                            OflineTime = entity.OflineTime,
                        };

                        if (Status)
                        {
                            await _pingLogRepository.Create(pingLog);
                            return true;
                        }
                    }
                }


                catch (Exception ex)
                {
                    _logger.LogError(ex, "Database error occurred while saving changes in addTimeInService.");
                    throw new Exception("Database error occurred while saving changes.", ex.InnerException ?? ex);
                }

                return false;
           
        }

        public async Task<List<PingLogDtoResponse>> GetAll()
        {
            try
            {
                var offlineAllUsers = await _pingLogRepository.GetAll();

                var pingLogDtoRequests = offlineAllUsers
                    .Where(log => log.OnlieTime != null && log.OflineTime.Any())
                    .Select(log => new PingLogDtoResponse
                    {
                        Id = log.Id,
                        OnlieTime = log.OnlieTime,
                        OflineTime = log.OflineTime?.ToList(),
                        _UserDto = log.User != null ? new UserDto
                        {
                            Id = log.User.Id,
                            Name = log.User.Name
                        } : null
                    })
                    .ToList();

                return pingLogDtoRequests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all PingLogs.");
                throw;
            }
        }

        private bool HasOnlineRecordForToday(PingLog existingLog)
        {
            return existingLog?.OnlieTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;
        }

        private bool HasSufficientTimePassed(PingLog existingLog)
        {
            return existingLog?.OnlieTime?.Count > 0 && !existingLog.OnlieTime.Any(time => time.Day == DateTime.Now.Day);
        }

        private bool HasOfflineRecordForToday(PingLog existingLog)
        {
            return existingLog?.OflineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;
        }
    }
}