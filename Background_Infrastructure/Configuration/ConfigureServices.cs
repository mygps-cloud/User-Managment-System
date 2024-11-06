
using Background_Infrastructure.Services;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Persistence;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Background_Infrastructure.Services;
using Ipstatuschecker.Dto;


namespace Ipstatuschecker.Background_Infrastructure.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddservicesPingBackground(this IServiceCollection services)
        {
            services.AddHostedService<PingBackgroundService>();

            services.AddScoped<PingIpChecker>();

            services.AddScoped<CheckIpStatuses>();

            services.AddScoped<IPstatusService, ServiceIPstatus>();
            services.AddScoped<IPstatusRepository, StatusIpRepository>();

            services.AddScoped<IPingLogRepository, PingLogRepository>();
            services.AddScoped<IPingLogService, CheckInOutservice>();

            services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
            services.AddScoped<IWorkScheduleService<WorkSchedule_ReqvestDto>, WorkScheduleService>();

            return services;
        }
    }

}