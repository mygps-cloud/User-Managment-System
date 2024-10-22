using ipstatuschecker;
using Ipstatuschecker.interfaces;
using System.Net.NetworkInformation;


namespace PingBackgroundServic
{
  

public class PingBackgroundService : BackgroundService
{
    private readonly ILogger<PingBackgroundService> _logger;
    private readonly List<IpStatus> _ipList;
    private readonly Iservices<IpStatus> iservices;

    public PingBackgroundService(ILogger<PingBackgroundService> logger)
    {
        _logger = logger;

       
        _ipList = new List<IpStatus>
        {
            new IpStatus { IpAddress = "192.168.100.2", Status = "Unknown" },
            new IpStatus { IpAddress = "192.168.1.3", Status = "Unknown" },
            new IpStatus { IpAddress = "192.168.100.4", Status = "Unknown" }
            , new IpStatus { IpAddress = "192.168.1.71", Status = "Unknown" }
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var currentTime = DateTime.Now.TimeOfDay;
            var startTime = new TimeSpan(21, 0, 0); 
            var endTime = new TimeSpan(22, 0, 0); 

            if (currentTime >= startTime && currentTime <= endTime)
            {
                _logger.LogInformation("Starting Ping Task...");

                var tasks = _ipList.Select(async ip =>
                {
                    ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";
                    _logger.LogInformation($"IP: {ip.IpAddress} is {ip.Status}");
                });

                await Task.WhenAll(tasks);

                _logger.LogInformation("Ping Task Completed.");
            }
            else
            {
                _logger.LogInformation("Outside of active pinging hours.");
            }

          
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
            _logger.LogError($"Error pinging {ipAddress}: {ex.Message}");
            return false;
        }
    }

 

}

}