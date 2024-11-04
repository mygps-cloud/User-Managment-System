using System.ComponentModel.DataAnnotations;
using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Dto
{
  
    
         public class UserDtoTest
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(10, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }
            
        public List<PingLogDtoReqvest>? PingLogDtoReqvest  = new();
        public List<WorkSchedule_ResponseDto> WorkSchedules = new();    
       
    }
    
}