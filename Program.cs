
using ipstatuschecker.PingServices;
using Ipstatuschecker.DbContextSql;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Interfaces;
using Ipstatuschecker.Persistence;
using Ipstatuschecker.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DbIpCheck>(options =>
    options.UseSqlite("Data Source=UserIpChecker.db"));



builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<PingBackgroundService>();

builder.Services.AddScoped<IQueryIpStatusRepository<User>,UserQueryRepository>();
builder.Services.AddScoped<ICommandIpStatusRepository<User>,UserCommandIRepository>();

builder.Services.AddScoped<Iservices<UserDto>, ServiceUser>();

builder.Services.AddScoped<DbPingBackgroundService>();
builder.Services.AddScoped<IPstatusIQueryPingDbRepository>();






var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
