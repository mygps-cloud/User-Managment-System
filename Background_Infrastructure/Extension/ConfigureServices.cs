
using Background_Infrastructure.Services;
using Background_Infrastructure.Services.UpdateService;
using ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Background_Infrastructure.HostService;
using Ipstatuschecker.Background_Infrastructure.Persistence;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Background_Infrastructure.Services.HostService;
using Ipstatuschecker.Background_Infrastructure.Services.TimeControlServices;
using Ipstatuschecker.Background_Infrastructure.Services.UpdateService;
using Ipstatuschecker.Dto;


namespace Ipstatuschecker.Background_Infrastructure.Extension
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddservicesPingBackground(this IServiceCollection services)
        {
            services.AddHostedService<PingBackgroundService>();
            // services.AddScoped(typeof(LockService<>));


            services.AddSingleton<PingIpChecker>();

            services.AddScoped<CheckIpStatuses>();

            services.AddScoped<UpdateCheckInOut>();
            services.AddScoped<UpdateBreake>();

            services.AddSingleton<ITimeControl<WorkSchedule_ReqvestDto, WorkScheduleResult>,
            TimeControlWorkScheduleService>();

            services.AddSingleton<ITimeControl<PingLogDtoReqvest, CheckInOutServiceResult>,
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