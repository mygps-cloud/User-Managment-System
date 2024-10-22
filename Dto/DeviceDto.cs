using System.ComponentModel.DataAnnotations;

namespace Ipstatuschecker.Dto
{
    public class DeviceDto
    {
          public int? Id { get; set; }

        [Required(ErrorMessage = "Device name is required.")]
        [StringLength(10, ErrorMessage = "Device name cannot be longer than 100 characters.")]
        public string? DeviceNames { get; set; }
        public int? UserId { get; set; }
        
    }
}