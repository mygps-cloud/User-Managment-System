using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstatuschecker.DomainEntity
{
   public class AttendanceLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } 
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string Notes { get; set; }
    public bool Approved { get; set; }
}

}