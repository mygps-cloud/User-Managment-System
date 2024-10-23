using System.ComponentModel.DataAnnotations;
using ipstatuschecker;

namespace Ipstatuschecker.Dto
{
    public class DeviceDto
    {
          public int? Id { get; set; }

        [Required(ErrorMessage = "Device name is required.")]
        [StringLength(10, ErrorMessage = "Device name cannot be longer than 100 characters.")]
   
    public string? DeviceNames { get; set; }

    public int? IpStatusId { get; set; } 
    public IpStatusDto? IpStatus { get; set; } 
    
    
        
        
    }
}