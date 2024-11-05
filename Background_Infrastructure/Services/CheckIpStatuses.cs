
using Ipstatuschecker.Abstractions.interfaces.IServices;
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
                var pingLogService = scope.ServiceProvider.GetRequiredService<IPingLogService>();
                 var workScheduleService = scope.ServiceProvider.GetRequiredService< IWorkScheduleService<WorkSchedule_ReqvestDto>>();

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

                            var WorkSchedule_Dto= new WorkSchedule_ReqvestDto
                            {
                                UserId = task.Id.Value,
                                StartTime = response ? new List<DateTime> () : new List<DateTime>{ DateTime.Now },
                                EndTime = response ? new List<DateTime>{ DateTime.Now }: new List<DateTime> ()

                            };
                          await  pingLogService.addPingLogService(pingLog);
                          await  workScheduleService.addBreakTime(WorkSchedule_Dto);
                        //   var task1 = Task.Run(() => pingLogService.addPingLogService(pingLog));
                        //   var task2 = Task.Run(() => pingLogService.addworkScheduleService(WorkSchedule_Dto));

                        //   await Task.WhenAll(task1, task2);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("No users found to ping.");
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
