namespace Ipstatuschecker.Dto
{
    public class PingLogDtoResponse
    {
        
    public int Id { get; set; }
    public DateTime OnlieTime { get; set; }
    public List< DateTime>? OflineTime { get; set; }
    public UserDto? User { get;set;}
    }

}