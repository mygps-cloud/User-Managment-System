

using Abstractions.interfaces.Iservices;
using Background_Infrastructure.Services;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Persistence;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Background_Infrastructure.Services;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.Services;

namespace Ipstatuschecker.Background_Infrastructure.Configuration
{
public static class  ConfigureServices
{
    public static IServiceCollection AddservicesPingBackground(this IServiceCollection services)
    {
        services.AddHostedService<PingBackgroundService>();

        services.AddSingleton<PingIpChecker>(); 

        services.AddSingleton<CheckIpStatuses>();

    
        services.AddScoped<PingLogCommandIRepository>();

        services.AddScoped<IPingLogRepository,PingLogRepository>();

        services.AddScoped<DbPingBackgroundService>();
        services.AddScoped<IPstatusIQueryPingDbRepository>();
        services.AddScoped<IPingLogService, CheckInOutservice>();
      
        services.AddScoped<IWorkScheduleRepository,WorkScheduleRepository>();
        services.AddScoped<IWorkScheduleService<WorkSchedule_ReqvestDto>,WorkScheduleService>();
        
        return services;
    }
}
}