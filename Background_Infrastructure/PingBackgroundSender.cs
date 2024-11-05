
using Ipstatuschecker.Background_Infrastructure.Services;

namespace Ipstatuschecker.Background_Infrastructure

{
public class PingBackgroundService : BackgroundService
{
  private readonly CheckIpStatuses checkIpStatuses;

    public PingBackgroundService(CheckIpStatuses checkIpStatuses)
    {
      
        this.checkIpStatuses=checkIpStatuses;
    }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await checkIpStatuses.CheckIpStatus();
                     await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
     }

 }
        
        
}

    
