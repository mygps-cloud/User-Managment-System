

using Ipstatuschecker.Abstractions.interfaces;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Background_Infrastructure.Services;
using Ipstatuschecker.Mvc.Infrastructure.Services;
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
        services.AddHostedService<PingBackgroundService>();
        services.AddScoped<IPingLogRepository,PingLogRepository>();
        return services;
    }
}
}