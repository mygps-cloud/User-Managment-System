namespace Ipstatuschecker.Dto
{
    public class ByNameViweModelDto
    {
         public int Id { get; set; } 
       
        public string? Name { get; set; }

       
        public PingLogDtoResponse? PingLogDtoResponse { get; set; } = new();
        public WorkSchedule_ResponseDto? WorkSchedules { get; set; } = new();   
        
    }
}