
using Microsoft.AspNetCore.Mvc;
using Ipstatuschecker.Dto;
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.Dto.Response;
using Ipstatuschecker.Mvc.Infrastructure.Services;
using Ipstatuschecker.Background_Infrastructure.Services.HostService;

namespace ipstatuschecker.Mvc.Presentacion.Controllers
{
    public class HomeController(IUserservices<UserDto> iservices, PingIpChecker pingIpChecker,IndexService indexService) : Controller
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
                var timesheetEntry = await statistic.HourStatistics(user);
                timesheetEntries.AddRange(timesheetEntry);
            }

            return View("~/Mvc/Presentacion/Views/Home/ByName.cshtml", timesheetEntries);
        }

        public async Task<IActionResult> Index()
        {
            var model = await indexService.Dai();

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

        [HttpGet("Home/SelectDevicePingInfo/{ipAddress}")]
        public async Task<IActionResult> SelectDevicePingInfo(string ipAddress)
        {

            var status = await pingIpChecker.PingIp(ipAddress);
            Console.WriteLine($"ipAddress   ,{ipAddress}");

            return Json(new { status = status ? "Online" : "Offline" });

        }
       

    }
}



