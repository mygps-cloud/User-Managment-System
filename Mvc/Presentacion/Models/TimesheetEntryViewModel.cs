namespace Ipstatuschecker.Mvc.Presentacion.Models
{
    public class TimesheetEntryViewModel
    {
        public DateTime Date { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime BreakStart { get; set; }
        public DateTime BreakEnd { get; set; }
        public DateTime TimeOut { get; set; }
        public string? BreakHours { get; set; }
        public string? TotalHours { get; set; }
        public string? TotalProductiveHours { get; set; }
    }
}
