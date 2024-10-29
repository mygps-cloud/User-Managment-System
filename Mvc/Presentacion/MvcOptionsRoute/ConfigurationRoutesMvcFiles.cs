using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Ipstatuschecker.Mvc.Presentacion.MvcOptionsRoute
{
    public static class ConfigurationRoutesMvcFiles
    {
        public static IServiceCollection RouteMvcOptions(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
            
                options.ViewLocationFormats.Add("/Mvc/Presentacion/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Mvc/Presentacion/Views/Shared/{0}.cshtml");
            });

            return services;
        }
    }
}
