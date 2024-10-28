
using System.Net.NetworkInformation;
using Ipstatuschecker.Dto;
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
                    await CheckIpStatuses();
                    await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
                }
     }
private async Task CheckIpStatuses()
{
    using (var scope = serviceProvider.CreateScope())
    {
        var _ipStatusService = scope.ServiceProvider.GetRequiredService<DbPingBackgroundService>();
        var _pingLogService = scope.ServiceProvider.GetRequiredService<PingLogService>();

        try
        {
            var tasksUsers = await _ipStatusService.GetAllUsers();

            if (tasksUsers.Count > 0)
            {
                foreach (var task in tasksUsers)
                {
                    var response = await PingIp(task.IpAddress);

                    var pingLog = new PingLogDtoReqvest
                    {
                        UserId = task.Id.Value,
                        OnlieTime = response ? new List<DateTime> { DateTime.Now } : new List<DateTime>{},
                        OflineTime = response ? new List<DateTime>{} : new List<DateTime> { DateTime.Now }
                    };

                 
                        await _pingLogService.AddNewUser(pingLog);
                    
                }
            }
            else
            {
                Console.WriteLine("No users found to ping.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}


 private async Task<bool> PingIp(string ipAddress)
{
    try
    {
        using (var ping = new Ping())
        {
            var reply = await ping.SendPingAsync(ipAddress);
            Console.WriteLine($"Pinged {ipAddress}, Status: {reply.Status}, Roundtrip Time: {reply.RoundtripTime} ms");
            
                return reply.Status == IPStatus.Success? true :false;
            
        }
    }
    catch (Exception ex)
    {
        logger.LogError($"Error pinging {ipAddress}: {ex.Message}");
        return false;
    }
}



 }
        
        


    
