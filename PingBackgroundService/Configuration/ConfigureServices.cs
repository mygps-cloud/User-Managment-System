using Ipstatuschecker.PingBackgroundService.Services;

public static class  ConfigureServices
{
    public static IServiceCollection AddservicesPingBackground(this IServiceCollection services)
    {
        services.AddSingleton<PingIpChecker>(); 
        services.AddSingleton<CheckIpStatuses>();
        services.AddHostedService<PingBackgroundService>();
        return services;
    }
}
