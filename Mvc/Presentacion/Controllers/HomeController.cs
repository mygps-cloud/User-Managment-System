
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using Ipstatuschecker.Dto;
using Abstractions.interfaces;



namespace ipstatuschecker.Mvc.Presentacion.Controllers
{
  

    public class HomeController(Iservices<UserDto> iservices) : Controller
    {


public async Task<IActionResult> Index()
{
    var model = await Dai(); 
    
   return View("~/Mvc/Presentacion/Views/Home/Index.cshtml", model);
 
   
}




public async Task<IActionResult> robika()
{
     
   return View();
}
    
[HttpGet]
  public async Task<IActionResult> PingIp13(string ipAddress)
      {
     //database
        
     var status = await PingIp("192.168.1.94");
     
    return Json(new { status = status ? "Online" : "Offline" });
  
     }

 
            public async Task<IActionResult> GetIpStatus()
            {
                var model = await Dai(); 
                return Json(model); 
            }


     private async Task<IEnumerable<UserDto>> Dai()
{
    var users = await iservices.GetAllUsers();
    
    // Console.WriteLine(users);
    var userDtos = new List<UserDto>();

    var tasks = users.Select(async user =>
    {
        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            IpStatuses = new List<IpStatusDto>(),
            Devices = new List<DeviceDto>()
        };

        foreach (var ipStatus in user.IpStatuses)
        {
            string ipAddress = ipStatus.IpAddress;

           
            string status = await PingIp(ipAddress) ? "Online" : "Offline";

            userDto.IpStatuses.Add(new IpStatusDto 
            { 
                IpAddress = ipAddress, 
                Status = status 
            });
        }

       
        foreach (var device in user.Devices)
        {
            userDto.Devices.Add(new DeviceDto
            {
                DeviceNames = device.DeviceNames 
            });
        }

        userDtos.Add(userDto);
    });

  
    await Task.WhenAll(tasks);

    return userDtos; 
}
       
          


        public async Task<bool> PingIp(string ipAddress)
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



