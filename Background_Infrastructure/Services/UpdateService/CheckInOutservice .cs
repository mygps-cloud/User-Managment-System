
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Services.TimeControlServices;
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
                    // var existingLog = await _pingLogRepository.GetByIdAsync(entity.UserId.Value);
                     var existingLog = await _context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);

                    if (existingLog == null)
                    {

                        var pingLog = new PingLog
                        {
                            UserId = entity.UserId,
                            OnlineTime = entity.OnlineTime ?? new List<DateTime>(),
                            OflineTime = entity.OflineTime ?? new List<DateTime>(),
                        };

                        if (Status)
                        {
                            await _pingLogRepository.Create(pingLog);
                            return true;
                        }


                        return false;
                    }


                    existingLog.OnlineTime ??= new List<DateTime>();
                    existingLog.OflineTime ??= new List<DateTime>();

                    var ServiceTime = _serviceProvider.GetRequiredService<ITimeControl<PingLogDtoReqvest, CheckInOutServiceResult>>();

                    var pingLogDtoReqvest = new PingLogDtoReqvest
                    {
                        UserId = existingLog.UserId,
                        OnlineTime = existingLog.OnlineTime,
                        OflineTime = existingLog.OflineTime
                    };

                    var Result = await ServiceTime.TimeControlResult(pingLogDtoReqvest, Status);

                    if (Result != null)
                    {
                        if (existingLog.OnlineTime.Count > 0 &&
                            !Result.HasOfflineRecordForToday &&
                            Result.LastTimeIn)
                        {
                            existingLog.OflineTime.Add(DateTime.Now);
                        }

                        return await _pingLogRepository.Save();
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

        public Task<List<PingLogDtoResponse>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
