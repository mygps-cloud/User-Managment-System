
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
      
        public CheckInOutservice(DbIpCheck context, 
        IPingLogRepository pingLogRepository, ILogger<CheckInOutservice> logger)
        {
            _context = context;
            _pingLogRepository = pingLogRepository;
            _logger = logger;
        }

        public async Task<bool> addTimeInService(PingLogDtoReqvest entity, bool Status)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
           
            //  var existingLog = await _pingLogRepository.GetByIdAsync(entity.UserId);
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
                            existingLog?.OnlineTime?.Add(DateTime.Now);
                        }

                        if (existingLog?.OnlineTime?.Count > 0 && !hasOfflineRecordForToday &&
                            (DateTime.Now - existingLog.OnlineTime.Last()).Minutes >= 5
                            && entity?.OflineTime?.Count > 0)
                        {
                            existingLog?.OflineTime?.Add(DateTime.Now);
                        }

                        return await _pingLogRepository.Save();
                    }
                    else
                    {
                        var pingLog = new PingLog
                        {
                            UserId = entity.UserId,
                            OnlineTime = entity.OnlieTime,
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



        private bool HasOnlineRecordForToday(PingLog existingLog)
        {
            return existingLog?.OnlineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;
        }

        private bool HasSufficientTimePassed(PingLog existingLog)
        {
            return existingLog?.OnlineTime?.Count > 0 && !existingLog.OnlineTime.Any(time => time.Day == DateTime.Now.Day);
        }

        private bool HasOfflineRecordForToday(PingLog existingLog)
        {
            return existingLog?.OflineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;
        }

        public Task<List<PingLogDtoResponse>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}