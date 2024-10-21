using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ipstatuschecker.Models;
using System.Net.NetworkInformation;
using PingBackgroundServic;

namespace ipstatuschecker.Controllers
{
  

    public class HomeController : Controller
    {

 private readonly PingBackgroundService _pingBackgroundService;

    public HomeController(PingBackgroundService pingBackgroundService)
    {
        _pingBackgroundService = pingBackgroundService;
    }

    public IActionResult Index()
    {
        var model = _pingBackgroundService.GetIpStatuses(); // IP სტატუსების მიღება
        return View(model);
    }
       
            public async Task<IActionResult> Privacy()
            {
               
                return View("Privacy"); 
            }


             [HttpGet]
            public async Task<IActionResult> GetIpStatus()
            {
                var model = await Dai(); 
                return Json(model); 
            }


        private async Task<IpCheckViewModel> Dai()
        {
            var ipList = new List<IpStatus>
            {
                new IpStatus { IpAddress = "192.168.100.2", Status = "Unknown" },
                new IpStatus { IpAddress = "192.168.1.3", Status = "Unknown" },
                new IpStatus { IpAddress = "192.168.100.4", Status = "Unknown" }
            };

           
            var tasks = ipList.Select(async ip =>
            {
                ip.Status = await PingIp(ip.IpAddress) ? "Online" : "Offline";
                return ip; 
            });

            await Task.WhenAll(tasks); 

            var model = new IpCheckViewModel
            {
                UserIpAddress = "Your IP Address Here", 
                IpAddresses = ipList,
                UserName = "Your User Name"
            };

            return model; 
        }

        private async Task<bool> PingIp(string ipAddress)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(ipAddress);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking {ipAddress}: {ex.Message}");
                return false;
            }
        }
    }
}



