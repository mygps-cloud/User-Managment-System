
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.Dto.Response;
using Ipstatuschecker.Mvc.Infrastructure.Services;



namespace ipstatuschecker.Mvc.Presentacion.Controllers
{


    public class HomeController(IUserservices<UserDto> iservices) : Controller
    {



        [HttpPost("Home/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }

            var result = await iservices.DelteUserById(id);

            if (result)
            {
                return Ok(new { message = "User deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "User not found or could not be deleted." });
            }
        }



        public async Task<IActionResult> ByName(int id)
        {
            var statistic = new UserStatisticServices();

            var users = await iservices.GetUserByIdTolist(id);

            var timesheetEntries = new List<TimesheetEntry>();

            foreach (var user in users)
            {
                var timesheetEntry = await statistic.HourStatistic(user);
                timesheetEntries.Add(timesheetEntry);
            }

            return View("~/Mvc/Presentacion/Views/Home/ByName.cshtml", timesheetEntries);
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
                    OnlineTime = p.PingLogDtoResponse.OnlineTime?.OrderByDescending(param => DateTime.Today).Reverse().ToList(),
                    OflineTime = p.PingLogDtoResponse.OflineTime?.OrderByDescending(param => DateTime.Today).Reverse().ToList()
                } : null,

                WorkSchedules = p.WorkSchedules != null ? new WorkSchedule_ResponseDto
                {
                    StartTime = p.WorkSchedules.StartTime?.OrderByDescending(param => DateTime.Today).Reverse().ToList(),
                    EndTime = p.WorkSchedules.EndTime?.OrderByDescending(param => DateTime.Today).Reverse().ToList()
                } : null
            }).ToList();

            return View("~/Mvc/Presentacion/Views/Home/Users.cshtml", breake);
        }



        [HttpGet("Home/PingIp13/{ipAddress}")]
        public async Task<IActionResult> PingIp13(string ipAddress)
        {


            var status = await PingIp(ipAddress);
            Console.WriteLine($"ipAddress   ,{ipAddress}");

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

    if (users == null || !users.Any())
    {
        return Enumerable.Empty<UserDto>(); 
    }

    var tasks = users.Select(async user =>
    {
        if (user == null)
        {
            return null; 
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.Name ?? string.Empty,
            IpStatuses = new List<IpStatusDto>(),
            Devices = new List<DeviceDto>()
        };

        if (user.IpStatuses != null)
        {
            foreach (var ipStatus in user.IpStatuses)
            {
                if (ipStatus != null)
                {
                    string ipAddress = ipStatus.IpAddress ?? string.Empty;

                    string status = await PingIp(ipAddress) ? "Online" : "Offline";

                    userDto.IpStatuses.Add(new IpStatusDto
                    {
                        IpAddress = ipAddress,
                        Status = status
                    });
                }
            }
        }

        if (user.Devices != null)
        {
            foreach (var device in user.Devices)
            {
                if (device != null)
                {
                    userDto.Devices.Add(new DeviceDto
                    {
                        DeviceNames = device.DeviceNames ?? string.Empty 
                    });
                }
            }
        }

        return userDto; 
    });

    var userDtos = await Task.WhenAll(tasks);

  
    return userDtos.Where(dto => dto != null)!; 
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



