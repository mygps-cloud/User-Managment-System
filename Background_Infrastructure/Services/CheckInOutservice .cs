
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;


namespace Background_Infrastructure.Services
{
    public class CheckInOutservice(DbIpCheck context,
    IPingLogRepository pingLogRepository)
    : IPingLogService
    {



        public async Task<bool> addTimeInService(PingLogDtoReqvest entity, bool Status)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //===============================================================================================================//

              var existingLog = await context.PingLog.FirstOrDefaultAsync(pl => pl.UserId == entity.UserId);

            // var existingLog = await pingLogRepository.GetByIdAsync(entity.UserId);



            var hasOnlineRecordForToday = existingLog?.OnlieTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;

            var hasSufficientTimePassed = existingLog?.OnlieTime?.Count > 0
            && !existingLog.OnlieTime.Any(time => time.Day == DateTime.Now.Day);
            var hasOfflineRecordForToday = existingLog?.OflineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;

            //==============================================================================================================//
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

                    return await pingLogRepository.Save();
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
                        await pingLogRepository.Create(pingLog);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error occurred while saving changes.", ex.InnerException ?? ex);
            }

            return false;
        }


        public async Task<List<PingLogDtoResponse>> GetAll()
        {
            {
                var offlineAllUsers = await pingLogRepository.GetAll();


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
        }


    }
}