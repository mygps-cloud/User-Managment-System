
/*
using ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;
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
        private readonly ITimeControl<PingLogDtoReqvest,CheckInOutServiceResult> _timeControl;
        public CheckInOutservice(DbIpCheck context,
         IPingLogRepository pingLogRepository,
        ILogger<CheckInOutservice> logger,
         ITimeControl<PingLogDtoReqvest,CheckInOutServiceResult> timeControl)
        {
            _context = context;
            _pingLogRepository = pingLogRepository;
            _logger = logger;
            _timeControl = timeControl;
        }

        public async Task<bool> addTimeInService(PingLogDtoReqvest entity, bool Status)
        {
       
             if (entity == null) throw new ArgumentNullException(nameof(entity));
            var existingLog = await _context.PingLog.AsNoTracking().FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);

            bool   LastTimeIn =  (DateTime.Now - existingLog.OnlieTime.Last()).Minutes >= 2;
             var checkInOutResult = await _timeControl.TimeControlResult(entity, Status);
         

            try
            {
                if (existingLog != null)
                {
                    if (!checkInOutResult.HasOnlineRecordForToday && Status)
                    {
                        existingLog?.OnlieTime?.Add(DateTime.Now);

                    }

                    if (existingLog?.OnlieTime?.Count > 0&& 
                    !checkInOutResult.HasOfflineRecordForToday
                     && LastTimeIn&& !Status)
                    {
                        existingLog?.OflineTime?.Add(DateTime.Now.AddMinutes(-2));
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


    }
}
*/


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
                            (DateTime.Now - existingLog.OnlieTime.Last()).Minutes >= 2
                            && entity?.OflineTime?.Count > 0)
                        {
                            existingLog?.OflineTime?.Add(DateTime.Now.AddMinutes(-2));
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