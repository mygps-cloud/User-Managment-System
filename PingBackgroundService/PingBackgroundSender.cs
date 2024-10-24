// using ipstatuschecker;
// using Ipstatuschecker.Dto;
// using Ipstatuschecker.interfaces;
// using System.Net.NetworkInformation;


// namespace PingBackgroundServic
// {
//     public class PingBackgroundService : BackgroundService
//     {
//         private readonly ILogger<PingBackgroundService> logger;
//         private readonly Iservices<IpStatusDto> ipStatusService;

//         public PingBackgroundService(Iservices<IpStatusDto> ipStatusService, 
//         ILogger<PingBackgroundService> logger)
//         {
//             this.ipStatusService = ipStatusService;
//             this.logger = logger;
//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             while (!stoppingToken.IsCancellationRequested)
//             {
//                 logger.LogInformation("Starting Ping Task...");

//                 var tasksUsers = await ipStatusService.GetAllUsers();
//                 var tasks = tasksUsers.Select(async ip =>
//                 {
//                     ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";
//                     logger.LogInformation($"IP: {ip.IpAddress} is {ip.Status}");
//                 });

//                 await Task.WhenAll(tasks);
//                 logger.LogInformation("Ping Task Completed.");
//                 await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
//             }
//         }

//         public async Task<bool> PingIp(string ipAddress)
//         {
//             try
//             {
//                 using (var ping = new Ping())
//                 {
//                     var reply = await ping.SendPingAsync(ipAddress);
//                     return reply.Status == IPStatus.Success;
//                 }
//             }
//             catch (Exception ex)
//             {
//                 logger.LogError($"Error pinging {ipAddress}: {ex.Message}");
//                 return false;
//             }
//         }
//     }
// }


using System.Net.NetworkInformation;

using Ipstatuschecker.Services;

public class PingBackgroundService : BackgroundService
{
    private readonly ILogger<PingBackgroundService> logger;
    private readonly IServiceProvider serviceProvider;

    public PingBackgroundService(IServiceProvider serviceProvider, ILogger<PingBackgroundService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Starting Ping Task...");

            using (var scope = serviceProvider.CreateScope())
            {
               var ipStatusService = scope.ServiceProvider.GetRequiredService<DbPingBackgroundService>();

                var tasksUsers = await ipStatusService.GetAllUsers();

                var tasks = tasksUsers.Select(async ip =>
                {
                    ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";
                    logger.LogInformation($"IP: {ip.IpAddress} is {ip.Status}");
                });

                await Task.WhenAll(tasks);
            }

            logger.LogInformation("Ping Task Completed.");
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    public async Task<bool> PingIp(string ipAddress)
    {
        try
        {
            using (var ping = new Ping())
            {
                var reply = await ping.SendPingAsync(ipAddress);
                return reply.Status == IPStatus.Success;
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error pinging {ipAddress}: {ex.Message}");
            return false;
        }
    }
}
