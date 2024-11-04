
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Abstractions.interfaces.Iservices;




namespace ipstatuschecker.Mvc.Presentacion.Controllers
{
  

    public class HomeController(IUserservices<UserDto> iservices,
    IPingLogRepository pingLogRepository) : Controller
    {


public async Task<IActionResult> Index()
{
    var model = await Dai(); 
    
   return View("~/Mvc/Presentacion/Views/Home/Index.cshtml", model);
 
   
}

public async Task<IActionResult> Users()
{
      var offlineAllUsers = await pingLogRepository.GetAll();
    
      var users = await iservices.GetAllUsers();

var breake = users.Select(p => new UserDto
{
    Id = p.Id,
    Name = p.Name,
 
    PingLogDtoResponse = p.PingLogDtoResponse != null ? new PingLogDtoResponse
    {
         Id = p.PingLogDtoResponse.Id,
         OnlieTime = p.PingLogDtoResponse.OnlieTime,
         OflineTime = p.PingLogDtoResponse.OflineTime
        
    }:null,

    WorkSchedules = p.WorkSchedules != null ? new WorkSchedule_ResponseDto
    {
        StartTime=p.WorkSchedules.StartTime,
        EndTime=p.WorkSchedules.EndTime,
      
    } : null
}).ToList();



        var pingLogDtoRequests = offlineAllUsers
            .Select(log => new PingLogDtoResponse
            {
                Id = log.Id,
                OnlieTime = log.OnlieTime,
                OflineTime = log.OflineTime.ToList(),
                _UserDto = log.User != null ? new UserDto
                {
                    Id = log.User.Id,
                    Name = log.User.Name
                } : null
            })
            .ToList();

        
     
  return View("~/Mvc/Presentacion/Views/Home/Users.cshtml",breake);
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



