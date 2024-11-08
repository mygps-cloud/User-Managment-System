using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Services.UpdateService;

namespace Ipstatuschecker.Background_Infrastructure.Services
{
    public class CheckIpStatuses 
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CheckIpStatuses> _logger;
        private readonly PingIpChecker _pingIpChecker;

        public CheckIpStatuses(IServiceProvider serviceProvider, ILogger<CheckIpStatuses> logger, PingIpChecker pingIpChecker)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _pingIpChecker = pingIpChecker;
        }

        public async Task CheckIpStatus()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var ipStatusService = scope.ServiceProvider.GetRequiredService<IPstatusService>();
                var pingIpChecker = scope.ServiceProvider.GetRequiredService<PingIpChecker>();
                var updatePingLogService = scope.ServiceProvider.GetRequiredService<UpdateCheckInOut>();
                var updateWorkScheduleService = scope.ServiceProvider.GetRequiredService<UpdateBreake>();

                try
                {
                    var tasksUsers = await ipStatusService.GetAllUsersWitchIp();

                    if (tasksUsers.Count > 0)
                    {
                        foreach (var task in tasksUsers)
                        {
                            var PingResponseStatus = await pingIpChecker.PingIp(task.IpAddress);

                            await updatePingLogService.UpdatePingLog(scope, task.Id.Value, PingResponseStatus);
                            await updateWorkScheduleService.UpdateWorkSchedule(scope, task.Id.Value, PingResponseStatus);
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