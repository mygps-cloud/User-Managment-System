namespace Ipstatuschecker.Dto
{
    public class WorkSchedule_ResponseDto
    {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public List<TimeSpan>? StartTime { get; set; }
    public List<TimeSpan>? EndTime { get; set; }
    public int BreakDuration { get; set; } 

    }
}