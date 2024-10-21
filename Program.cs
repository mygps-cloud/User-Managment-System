
using ipstatuschecker.PingServices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Interfaces;
using Ipstatuschecker.Services;
using PingBackgroundServic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<PingBackgroundService>();
builder.Services.AddScoped<PingService>();
builder.Services.AddScoped<IQueryIpStatusRepository<User>>();
builder.Services.AddScoped<ICommandIpStatusRepository<User>>();

builder.Services.AddScoped<Iservices<UserDto>, ServiceIpStatus>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
