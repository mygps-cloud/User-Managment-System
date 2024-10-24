using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstatuschecker.DomainEntity
{
    public class DataTimeForUsersControl
    { 
   public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public List<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
    public List<PingLog> PingLogs { get; set; } = new List<PingLog>();



    }
}

