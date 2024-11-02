namespace Ipstatuschecker.Dto
{
    public class WorkSchedule_ReqvestDto
    {
   public int Id { get; set; }
  
   
    public List<DateTime>? StartTime { get; set; }
    public List<DateTime>? EndTime { get; set; }
    public int BreakDuration { get; set; } 

     public int UserId { get; set; } 
    }
}