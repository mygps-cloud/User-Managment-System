
using Ipstatuschecker.Background_Infrastructure.Configuration;
using Ipstatuschecker.Background_Infrastructure.RouteServices.MinimalApiService;
using Ipstatuschecker.Mvc.Infrastructure.Configuration;
using Ipstatuschecker.Mvc.Presentacion.MvcOptionsRoute;


var builder = WebApplication.CreateBuilder(args);
 builder.Services.AddEndpointsApiExplorer();



 builder.Services.AddHealthChecks();
    

// builder.Services.AddDbContext<DbIpCheck>(options =>
//     options.UseSqlite("Data Source=UserIpChecker.db"));



builder.Services.AddControllersWithViews();
builder.Services.AddservicesPingBackground();
builder.Services.RouteMvcOptions();
builder.Services.AddDbConfiguration();

// builder.Services.AddScoped<DbPingBackgroundService>();

// builder.Services.AddScoped<IQueryIpStatusRepository<User>,UserQueryRepository>();
// builder.Services.AddScoped<ICommandIpStatusRepository<User>,UserCommandIRepository>();

// builder.Services.AddScoped<Iservices<UserDto>, ServiceUser>();

// builder.Services.AddScoped<DbPingBackgroundService>();
// builder.Services.AddScoped<IPstatusIQueryPingDbRepository>();


// builder.Services.AddScoped<PingLogService>();
//   builder.Services.AddScoped<PingLogCommandIRepository>();



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