
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Persistence;

namespace Ipstatuschecker.MinimalApiService
{
    public static class EndpointsServices
    {
       public static void PingLogEndpointsServices(this IEndpointRouteBuilder app)
       {
        var bookGroup = app.MapGroup("Pinglog");
        
        bookGroup.MapGet("GetAll", GetAll).WithName(nameof(GetAll));
       bookGroup.MapGet("{id}", GetById).WithName(nameof(GetById));

        bookGroup.MapGet("GetByName", GetByName).WithName(nameof(GetByName));
        bookGroup.MapPost("AddNewTimeStatus", AddNewTimeStatus).WithName(nameof(AddNewTimeStatus));

       }
public static async Task<List<PingLogDtoResponse>> GetAll(PingLogCommandIRepository pingLogCommandIRepository)
{  
    

    var offlineAllUsers = await pingLogCommandIRepository.GetAll();

    var pingLogDtoRequests = offlineAllUsers
        .Where(log => log.OnlieTime != null && log.OflineTime.Any())
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

    return pingLogDtoRequests; 
}




public static async Task<List<PingLogDtoResponse>> GetById(int id,PingLogCommandIRepository pingLogCommandIRepository)
{
 
 
            var ById= await pingLogCommandIRepository.GetByIdAsync(id);
              if (ById == null)
                {
                    throw new Exception("User not found."); 
                }

             var pingLogById =  new PingLogDtoResponse
              {
            Id = ById.Id,
            OnlieTime = ById.OnlieTime, 
            OflineTime = ById.OflineTime.ToList(), 
            _UserDto = ById.User!= null ? new UserDto
            {
                Id = ById.User.Id,
                Name = ById.User.Name 
            } : null 
              };
    

            return new List<PingLogDtoResponse> { pingLogById };
}



public static async Task<List<PingLogDtoResponse>> GetByName(string name,PingLogCommandIRepository pingLogCommandIRepository)
{
 
 
            var ById= await pingLogCommandIRepository.GetByNameAsync(name);

             var pingLogByName =  new PingLogDtoResponse
              {
            Id = ById.Id,
            OnlieTime = ById.OnlieTime, 
            OflineTime = ById.OflineTime.ToList(), 
            _UserDto = ById.User!= null ? new UserDto
            {
                Id = ById.User.Id,
                Name = ById.User.Name 
            } : null 
              };
    

            return new List<PingLogDtoResponse> { pingLogByName };
}


   public static async Task<bool> AddNewTimeStatus(PingLogDtoReqvest entity,PingLogCommandIRepository pingLogCommandIRepository)
{
    try
    {
       
        
        var pingLog = new PingLog
        {
            OnlieTime = entity.OnlieTime,
            OflineTime = entity.OflineTime,
            UserId = entity.UserId
        };
        await pingLogCommandIRepository.CreateUser(pingLog);

        return true;
    }
    catch (Exception ex)
    {
        throw new Exception("Database error ->>>", ex);
    }
}




    }


}