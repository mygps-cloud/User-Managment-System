
using Background_Infrastructure.Services;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.Services;


namespace Ipstatuschecker.Background_Infrastructure.Services
{
    public class CheckIpStatuses
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CheckIpStatuses> _logger;
        private readonly  PingIpChecker _pingIpChecker;

        public CheckIpStatuses(IServiceProvider serviceProvider, ILogger<CheckIpStatuses> logger,PingIpChecker pingIpChecker)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _pingIpChecker=pingIpChecker;
        }

        public async Task CheckIpStatus()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var ipStatusService = scope.ServiceProvider.GetRequiredService<DbPingBackgroundService>();
                var pingLogService = scope.ServiceProvider.GetRequiredService<PingLogService>();

                try
                {
                    var tasksUsers = await ipStatusService.GetAllUsers();

                    if (tasksUsers.Count > 0)
                    {
                        foreach (var task in tasksUsers)
                        {
                            var response = await _pingIpChecker.PingIp(task.IpAddress);

                            var pingLog = new PingLogDtoReqvest
                            {
                                UserId = task.Id.Value,
                                OnlieTime = response ? new List<DateTime> { DateTime.Now } : new List<DateTime>(),
                                OflineTime = response ? new List<DateTime>() : new List<DateTime> { DateTime.Now }
                            };

                            await pingLogService.addService(pingLog);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("No users found to ping.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred: {ex.Message}");
                }
            }
        }

      
    }
}
