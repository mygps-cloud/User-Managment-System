namespace Ipstatuschecker.Dto
{
   public class PingLogDtoReqvest
{
    public int Id { get; set; }
    public DateTime OnlieTime { get; set; }
    public List< DateTime>? OflineTime { get; set; }
    public int UserId { get; set; }
}

}