namespace Ipstatuschecker.Dto
{
    public class TimesheetEntry
    {
    public DateTime Date { get; set; }
    public DateTime TimeIn { get; set; }
    public DateTime BreakStart { get; set; }
    public DateTime BreakEnd { get; set; }
    public DateTime TimeOut { get; set; }
    public DateTime BreakHours { get; set; }
    public TimeSpan TotalHours { get; set; }
    public TimeSpan TotalProductiveHours { get; set; }
    }
}