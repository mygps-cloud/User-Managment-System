
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.Dto.Response;


public class TimesheetEntry
{
    public DateTime Date { get; set; }
    public DateTime TimeIn { get; set; }
    public DateTime Break1Start { get; set; }
    public DateTime Break1End { get; set; }
    public DateTime Break2Start { get; set; }
    public DateTime Break2End { get; set; }
    public DateTime TimeOut { get; set; }
    public TimeSpan TotalHours { get; set; }
    public TimeSpan Break1Hours { get; set; }
    public TimeSpan Break2Hours { get; set; }
    public TimeSpan TotalProductiveHours { get; set; }
}


namespace ipstatuschecker.Mvc.Presentacion.Controllers
{
  

    public class HomeController(IUserservices<UserDto> iservices,
    IPingLogRepository pingLogRepository) : Controller
    {


public async Task<IActionResult> ByName()
{
   
     var timesheetEntries = new List<TimesheetEntry>
        {
            new TimesheetEntry
            {
                Date = new DateTime(2022, 4, 1),
                TimeIn = DateTime.Parse("08:55 PM"),
                Break1Start = DateTime.Parse("10:58 PM"),
                Break1End = DateTime.Parse("11:19 PM"),
                Break2Start = DateTime.Parse("01:23 AM"),
                Break2End = DateTime.Parse("01:40 AM"),
                TimeOut = DateTime.Parse("05:15 AM"),
                TotalHours = TimeSpan.Parse("08:20:02"),
                Break1Hours = TimeSpan.Parse("00:21:04"),
                Break2Hours = TimeSpan.Parse("00:16:55"),
                TotalProductiveHours = TimeSpan.Parse("07:42:03")
            },
            // Add other entries similarly
        };
       
     
//   return View("~/Mvc/Presentacion/Views/Home/Users.cshtml",breake);
  return View("~/Mvc/Presentacion/Views/Home/ByName.cshtml",timesheetEntries);
}


public async Task<IActionResult> Index()
{
    var model = await Dai(); 
    
   return View("~/Mvc/Presentacion/Views/Home/Index.cshtml", model);
 
   
}

public async Task<IActionResult> Users()
{
    
      var users = await iservices.GetAllUsers();

var breake = users.Select(p => new GetAllViweModelDto
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



