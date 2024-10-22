using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using System.Net.NetworkInformation;


namespace ipstatuschecker.Controllers
{
  

    public class HomeController : Controller
    {



//  private readonly PingService PingService;

//     public HomeController(PingService PingService)
//     {
//       this.PingService = PingService;
//     }



public async Task<IActionResult> Index()
{
    var model = await Dai(); 
    return View(model);
}


// public async Task<IActionResult> robika()
// {
     
//     return View();
// }
    
// [HttpGet]
//   public async Task<IActionResult> PingIp2(string ipAddress)
//       {
//      //database
        
//      var status = await PingService.PingIp(ipAddress);
     
//     return Json(new { status = status ? "Online" : "Offline" });
  
//      }

 
            public async Task<IActionResult> GetIpStatus()
            {
                var model = await Dai(); 
                return Json(model); 
            }


        private async Task<IpCheckViewModel> Dai()
        {
              //database
            var ipList = new List<IpStatus>
            {
                new IpStatus { IpAddress = "192.168.1.94", Status = "Unknown" },
                new IpStatus { IpAddress = "192.168.1.106", Status = "Unknown" },
                new IpStatus { IpAddress = "192.168.1.75", Status = "Unknown" },
                 new IpStatus { IpAddress = "192.168.1.123", Status = "Unknown" },
                  new IpStatus { IpAddress = "192.168.1.71", Status = "Unknown" }
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



