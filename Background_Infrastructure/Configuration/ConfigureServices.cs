
using Background_Infrastructure.Services;
using ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.Persistence;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Background_Infrastructure.Services;
using Ipstatuschecker.Background_Infrastructure.Services.TimeControlServices;
using Ipstatuschecker.Dto;


namespace Ipstatuschecker.Background_Infrastructure.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddservicesPingBackground(this IServiceCollection services)
        {
            services.AddHostedService<PingBackgroundService>();

            services.AddSingleton<PingIpChecker>();

            services.AddScoped<CheckIpStatuses>();


            services.AddSingleton<ITimeControl<WorkSchedule_ReqvestDto, WorkScheduleResult>,
            TimeControlWorkScheduleService>();

            services.AddSingleton<ITimeControl<PingLogDtoReqvest,CheckInOutServiceResult>,
            CheckInOutserviceTimeControlService>();

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