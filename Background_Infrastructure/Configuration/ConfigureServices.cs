

using Background_Infrastructure.Services;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Background_Infrastructure.Persistence;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Background_Infrastructure.Services;
using Ipstatuschecker.Mvc.Infrastructure.Services;

namespace Ipstatuschecker.Background_Infrastructure.Configuration
{
public static class  ConfigureServices
{
    public static IServiceCollection AddservicesPingBackground(this IServiceCollection services)
    {
        services.AddSingleton<PingIpChecker>(); 
        services.AddSingleton<TimeControlService>(); 
        services.AddSingleton<CheckIpStatuses>();
        services.AddScoped<PingLogService>();
        services.AddScoped<PingLogCommandIRepository>();
        services.AddHostedService<PingBackgroundService>();
        services.AddScoped<IPingLogRepository,PingLogRepository>();
        services.AddScoped<DbPingBackgroundService>();
        services.AddScoped<IPstatusIQueryPingDbRepository>();
        services.AddScoped<IWorkScheduleRepository,WorkScheduleRepository>();
        
        return services;
    }
}
}