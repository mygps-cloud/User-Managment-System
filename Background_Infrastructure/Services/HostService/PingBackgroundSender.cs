
using Ipstatuschecker.Background_Infrastructure.Services;

namespace Ipstatuschecker.Background_Infrastructure.HostService

{public class PingBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<PingBackgroundService> _logger;

    public PingBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<PingBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var checkIpStatuses = scope.ServiceProvider.GetRequiredService<CheckIpStatuses>();
                

                try
                {
                    await checkIpStatuses.CheckIpStatus();
                    
                   
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred while checking IP statuses: {ex.Message}");
                }
            }

              await Task.Delay(3000, stoppingToken);
        }
    }
}

        
        
}

    
