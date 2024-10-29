

using Ipstatuschecker.PingBackgroundService.Services;
using Ipstatuschecker.Services;
using Mvc.Infrastructure.Persistence;

namespace Ipstatuschecker.Background_Infrastructure.Configuration
{
public static class  ConfigureServices
{
    public static IServiceCollection AddservicesPingBackground(this IServiceCollection services)
    {
        services.AddSingleton<PingIpChecker>(); 
        services.AddSingleton<CheckIpStatuses>();
        services.AddScoped<PingLogService>();
        services.AddScoped<PingLogCommandIRepository>();
        // services.AddHostedService<PingBackgroundService>();
        return services;
    }
}
}