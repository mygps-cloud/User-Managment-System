using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Services.UpdateService;

namespace Ipstatuschecker.Background_Infrastructure.Services.HostService
{
    public class TimeInTimeOut
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TimeInTimeOut> _logger;
        private readonly PingIpChecker _pingIpChecker;

        public TimeInTimeOut(IServiceProvider serviceProvider, ILogger<TimeInTimeOut> logger, PingIpChecker pingIpChecker)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _pingIpChecker = pingIpChecker;
        }

        public async Task AsyncMethodTimeInTimeOut()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var ipStatusService = scope.ServiceProvider.GetRequiredService<IPstatusService>();
                var pingIpChecker = scope.ServiceProvider.GetRequiredService<PingIpChecker>();
                var updatePingLogService = scope.ServiceProvider.GetRequiredService<UpdateCheckInOut>();
                try
                {
                    var tasksUsers = await ipStatusService.GetAllUsersWitchIp();
                    if (tasksUsers != null)
                    {
                        foreach (var task in tasksUsers)
                        {
                            if (string.IsNullOrEmpty(task.IpAddress) || task.Id == null)continue;
                            
                            var PingResponseStatus = await pingIpChecker.PingIp(task.IpAddress);

                            await updatePingLogService.UpdatePingLog(scope, task.Id.Value, PingResponseStatus);

                        }
                    }
                    else
                    {
                        _logger.LogWarning("No users found to ping.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}