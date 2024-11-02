namespace Ipstatuschecker.Dto
{
    public class PingLogDtoResponse
    {
        
    public int Id { get; set; }
    public List< DateTime>? OnlieTime { get; set; }
    public List< DateTime>? OflineTime { get; set; }
    public UserDto? _UserDto { get;set;}

    
    }

}