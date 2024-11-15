
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;
using ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;

namespace Background_Infrastructure.Services.UpdateService
{
    public class CheckInOutservice : IPingLogService
    {
        private readonly DbIpCheck _context;
        private readonly IPingLogRepository _pingLogRepository;
        private readonly ILogger<CheckInOutservice> _logger;
        private readonly IServiceProvider _serviceProvider;

        public CheckInOutservice(DbIpCheck context,
                                 IPingLogRepository pingLogRepository,
                                 IServiceProvider serviceProvider,
                                 ILogger<CheckInOutservice> logger)
        {
            _context = context;
            _pingLogRepository = pingLogRepository;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
public async Task<bool> addTimeInService(PingLogDtoReqvest entity, bool Status)
{
    if (entity == null) throw new ArgumentNullException(nameof(entity));

    try
    {
        if (entity.UserId.HasValue)
        {
            var existingLog = await _context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);

            if (existingLog != null)
            {
                existingLog.OnlineTime ??= new List<DateTime>();
                existingLog.OflineTime ??= new List<DateTime>();

              
                if (!existingLog.OnlineTime.Any(ot => ot.Date == DateTime.Today) && Status)
                {
                    existingLog.OnlineTime.Add(DateTime.Now);
                    return await _pingLogRepository.Save();
                }

                var ServiceTime = _serviceProvider.GetRequiredService<ITimeControl<PingLogDtoReqvest, CheckInOutServiceResult>>();
                if (ServiceTime == null)
                {
                    throw new InvalidOperationException("Time control service is not available.");
                }

                var pingLogDtoReqvest = new PingLogDtoReqvest
                {
                    UserId = existingLog.UserId,
                    OnlineTime = existingLog.OnlineTime,
                    OflineTime = existingLog.OflineTime
                };

                var Result = await ServiceTime.TimeControlResult(pingLogDtoReqvest, Status);

                if (Result != null)
                {
                    if (existingLog.OnlineTime.Any(ot => ot.Date == DateTime.Today) && !Status
                        && (DateTime.Now - existingLog.OnlineTime.Last()).Hours >= 3 && !Result.HasOfflineRecordForToday)
                    {
                        existingLog.OflineTime.Add(DateTime.Now);
                        return await _pingLogRepository.Save();
                    }

                    return await _pingLogRepository.Save();
                }
            }
            else
            {
               
                if (Status)
                {
                    var pingLog = new PingLog
                    {
                        UserId = entity.UserId,
                        OnlineTime = entity.OnlineTime ?? new List<DateTime>(),
                        OflineTime = entity.OflineTime ?? new List<DateTime>(),
                    };

                    await _pingLogRepository.Create(pingLog);
                    return true;
                }

                return false;
            }
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Database error occurred while saving changes in addTimeInService.");
        throw new InvalidOperationException("An error occurred while saving changes in addTimeInService.", ex);
    }

    return false;
}

    }
}
