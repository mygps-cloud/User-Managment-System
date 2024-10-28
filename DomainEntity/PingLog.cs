using System.Collections.Generic;


namespace Ipstatuschecker.DomainEntity
{
   public class PingLog
{
    public int Id { get; set; }
    public List<DateTime>?OnlieTime { get; set; }
    public List< DateTime>? OflineTime { get; set; }
    public int UserId { get; set; } 
    public User? User { get; set; }
}

}