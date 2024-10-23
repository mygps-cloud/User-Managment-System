using System.ComponentModel.DataAnnotations;

namespace Ipstatuschecker.Dto
{
    public class IpStatusDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "IP Address is required.")]
        [RegularExpression(@"^(\d{1,3}\.){3}\d{1,3}$", ErrorMessage = "Invalid IP Address format.")]
        public string? IpAddress { get; set; }

        public string? Status { get; set; }
        
        public int? DeviceId { get; set; } 
         public DeviceDto? DeviceDto { get; set; } 
    }
}
