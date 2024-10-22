
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KakuaController(Iservices<UserDto> iservices) : Controller
    {


        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


[HttpPost]
public async Task<IActionResult> Create([FromBody] UserDto userDto)
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

    
    return Ok(userDto); // ან შეგიძლიათ დაბრუნოთ RedirectToAction("Index");
}



        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}