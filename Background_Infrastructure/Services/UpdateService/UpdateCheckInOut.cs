using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Background_Infrastructure.Services.UpdateService
{
    public class UpdateCheckInOut
    {
        public async Task UpdatePingLog(IServiceScope scope, int userId, bool pingResponseStatus)
        {
            var pingLogService = scope.ServiceProvider.GetRequiredService<IPingLogService>();

            var PingLog = new PingLogDtoReqvest
            {
                UserId = userId,
                OnlineTime = pingResponseStatus ? new List<DateTime> { DateTime.Now } : new List<DateTime>(),
                OflineTime = pingResponseStatus ? new List<DateTime>(): new List<DateTime> { DateTime.Now }
            };

            await pingLogService.addTimeInService(PingLog, pingResponseStatus);
        }


    }
}