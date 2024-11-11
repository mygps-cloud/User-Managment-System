
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.Dto;
using Microsoft.AspNetCore.Mvc;


namespace ipstatuschecker.Mvc.Presentacion.Kakua
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagmentController(IUserservices<UserDto> iservices)
     : Controller
    {


    



        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] UserDto userDto)

        {

            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            var result = await iservices.AddNewUser(userDto);
            if (!result)
            {
                return StatusCode(500, "Internal server error while creating the user.");
            }


            return CreatedAtAction(nameof(Create), new { id = userDto.Id }, userDto);
        }



        //=================================================================//
        public async Task<IActionResult> CreateNewUser()
        {

            return View();
        }
    }
}