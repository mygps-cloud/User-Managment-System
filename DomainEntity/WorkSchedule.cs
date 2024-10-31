

namespace Ipstatuschecker.DomainEntity
{
    public class WorkSchedule
{
    public int Id { get; set; }
  
    public DateTime Date { get; set; }
    public List<TimeSpan>? StartTime { get; set; }
    public List<TimeSpan>? EndTime { get; set; }
    public int BreakDuration { get; set; } 

     public int UserId { get; set; } 
    //  public User? User { get; set; }
}

}