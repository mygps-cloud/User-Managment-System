using System.ComponentModel.DataAnnotations;
using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Dto
{
  
    
         public class UserDto
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(10, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

       
        public List<IpStatusDto>? IpStatuses { get; set; } = new();

      
        public List<DeviceDto>? Devices { get; set; } = new();
        //   public WorkScheduleDto? workSchedule {get;set;}
    }
    
}