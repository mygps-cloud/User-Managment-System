
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Background_Infrastructure.Persitence;
using Ipstatuschecker.Dto;


namespace Ipstatuschecker.Background_Infrastructure.RouteServices.MinimalApiService
{
    public static class EndpointsServices
    {
       public static void PingLogEndpointsServices(this IEndpointRouteBuilder app)
       {
        var bookGroup = app.MapGroup("Pinglog");
        
        bookGroup.MapGet("GetAll", GetAll).WithName(nameof(GetAll));
        bookGroup.MapGet("{id}", GetById).WithName(nameof(GetById));
        bookGroup.MapGet("GetByName/{name}", GetByName).WithName(nameof(GetByName));

       }
public static async Task<List<PingLogDtoResponse>> GetAll(IPingLogRepository pingLogRepository)
{  
    
 var offlineAllUsers= await pingLogRepository.GetAll();


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
 
 
            var ByName= await pingLogCommandIRepository.GetByNameAsync(name);
             if (ByName == null)
                {
                    throw new Exception("User not found."); 
                }

             var pingLogByName =  new PingLogDtoResponse
              {
            Id = ByName.Id,
            OnlieTime = ByName.OnlieTime, 
            OflineTime = ByName.OflineTime.ToList(), 
            _UserDto = ByName.User!= null ? new UserDto
            {
                Id = ByName.User.Id,
                Name = ByName.User.Name 
            } : null 
              };
    

            return new List<PingLogDtoResponse> { pingLogByName };
}



    }


}