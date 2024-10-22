
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KakuaController(Iservices<UserDto> iservices) : Controller
    {


        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            try
            {
                var result = await iservices.GetAllUsers();
                Console.WriteLine(result);
                
                if (result == null || !result.Any())
                {
                    return NotFound("No users found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


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




        [HttpPut("{id}")]
        public void Put(int id, [FromForm] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


//=================================================================//
        public async Task<IActionResult> robika()
            {
                
                return View();
            }
    }
}