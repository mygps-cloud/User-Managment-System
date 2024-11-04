namespace Ipstatuschecker.Dto.Response
{
    public class GetAllViweModelDto
    {
         public int Id { get; set; } 
       
        public string? Name { get; set; }
       
        public PingLogDtoResponse? PingLogDtoResponse { get; set; } = new();
        public WorkSchedule_ResponseDto? WorkSchedules { get; set; } = new();   
    }
}