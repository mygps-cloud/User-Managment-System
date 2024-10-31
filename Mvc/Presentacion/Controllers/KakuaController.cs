
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Dto;
using Microsoft.AspNetCore.Mvc;


namespace ipstatuschecker.Mvc.Presentacion.Kakua
{
    [Route("api/[controller]")]
    [ApiController]
    public class KakuaController(IUserservices<UserDto> iservices,PingLogCommandIRepository pingLogCommandIRepository) : Controller
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

   [HttpGet("GetAllUsers2")]
 public async Task<List<PingLogDtoResponse>> GetAllUsers2()
{
    var offlineAllUsers = await pingLogCommandIRepository.GetAll();

    var pingLogDtoRequests = offlineAllUsers
        .Where(log => log.OnlieTime != null && log.OflineTime.Any())
        .Select(log => new PingLogDtoResponse
        {
            Id = log.Id,
            OnlieTime = log.OnlieTime, 
            OflineTime = log.OflineTime, 
            _UserDto = log.User != null ? new UserDto
            {
                Id = log.User.Id,
                Name = log.User.Name 
            } : null 
        })
        .ToList();

    return pingLogDtoRequests;
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