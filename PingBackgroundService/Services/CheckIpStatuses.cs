

namespace Ipstatuschecker.PingBackgroundService.Services
{
    public class CheckIpStatuses
    {

// public async Task StartCheckingIpStatuses()
// {
//     while (true)
//     {
//         await CheckIpStatuses();
//         await Task.Delay(TimeSpan.FromSeconds(5));
//     }
// }

// private async Task CheckIpStatuses()
// {
//     var tasksUsers = await _ipStatusService.GetAllUsers();
//     if (tasksUsers.Count > 0)
//     {
//         var tasks = tasksUsers.Select(ip => Task.Run(async () =>
//         {
//             using (var newScope = scope.ServiceProvider.CreateScope())
//             {
//                 var _PingLogService = newScope.ServiceProvider.GetRequiredService<PingLogService>();

//                 ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";

//                 var pingLog = new PingLogDtoReqvest
//                 {
//                     OnlineTime = DateTime.Now,
//                     OfflineTime = null
//                 };

//                 if (ip.Status == "Offline")
//                 {
//                     pingLog.OfflineTime = DateTime.Now;
//                     Console.WriteLine($"IP {ip.IpAddress} is offline at {DateTime.Now}");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"IP {ip.IpAddress} is online at {DateTime.Now}");
//                 }

//                 _PingLogService.AddNewUser(pingLog);
//             }
//         }));

//         await Task.WhenAll(tasks);
//     }
// }

// private Timer _timer;

// public void StartTimer()
// {
//     _timer = new Timer(async _ => await CheckIpStatuses(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
// }

// private async Task CheckIpStatuses()
// {
//     var tasksUsers = await _ipStatusService.GetAllUsers();
//     if (tasksUsers.Count > 0)
//     {
//         var tasks = tasksUsers.Select(ip => Task.Run(async () =>
//         {
//             using (var newScope = scope.ServiceProvider.CreateScope())
//             {
//                 var _PingLogService = newScope.ServiceProvider.GetRequiredService<PingLogService>();

//                 ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";

//                 var pingLog = new PingLogDtoReqvest
//                 {
//                     OnlineTime = DateTime.Now,
//                     OfflineTime = null
//                 };

//                 if (ip.Status == "Offline")
//                 {
//                     pingLog.OfflineTime = DateTime.Now;
//                     Console.WriteLine($"IP {ip.IpAddress} is offline at {DateTime.Now}");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"IP {ip.IpAddress} is online at {DateTime.Now}");
//                 }

//                 _PingLogService.AddNewUser(pingLog);
//             }
//         }));

//         await Task.WhenAll(tasks);
//     }
// }





    }
}



