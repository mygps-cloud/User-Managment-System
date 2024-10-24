

namespace Ipstatuschecker.DomainEntity
{
   public class PingLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime PingTime { get; set; }
    public DateTime? ResponseTime { get; set; }
    public string Status { get; set; } 
    public int? ResponseCode { get; set; }
    public TimeSpan? Latency { get; set; }
}

}