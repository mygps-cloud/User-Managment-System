
using Ipstatuschecker.Background_Infrastructure.Configuration;
using Ipstatuschecker.Background_Infrastructure.RouteServices.MinimalApiService;
using Ipstatuschecker.Mvc.Infrastructure.DLA;
using Ipstatuschecker.Mvc.Presentacion.MvcOptionsRoute;


var builder = WebApplication.CreateBuilder(args);
 builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHealthChecks();

builder.Services.AddControllersWithViews();
builder.Services.AddservicesPingBackground();
builder.Services.RouteMvcOptions();
builder.Services.AddDatabase();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute
(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    
);

app.PingLogEndpointsServices();

app.Run();