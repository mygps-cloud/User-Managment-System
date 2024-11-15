using Abstractions.interfaces.Iservices;
using Ipstatuschecker.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ipstatuschecker.Mvc.Presentacion.Kakua
{
    public class UserManagmentController : Controller
    {
        private readonly IUserservices<UserDto> _iservices;

        public UserManagmentController(IUserservices<UserDto> iservices)
        {
            _iservices = iservices;
        }

        [HttpPost("UserManagment/Create")]
        public async Task<IActionResult> Create([FromForm] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            var result = await _iservices.AddNewUser(userDto);
            if (!result)
            {
                return StatusCode(500, "Internal server error while creating the user.");
            }

            return RedirectToAction("CreateNewUser");
        }

        //=================================================================//
        public IActionResult CreateNewUser()
        {
            return View("~/Mvc/Presentacion/Views/UserManagment/CreateNewUser.cshtml");
        }
    }
}
