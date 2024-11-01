using System.Net.NetworkInformation;
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.Mvc.Infrastructure.DLA.DbContextSql;
using Microsoft.EntityFrameworkCore;

namespace Ipstatuschecker.Background_Infrastructure.Services
{
    public class BreakTimeService(DbIpCheck context,
    IWorkScheduleRepository workScheduleRepository) : IWorkScheduleService
    {
        public async Task addBreakTime(WorkSchedule_ReqvestDto workSchedule_ReqvestDto)
      {
    if (workSchedule_ReqvestDto == null)throw new ArgumentNullException(nameof(workSchedule_ReqvestDto));

        try
        {
            var pingLog = new WorkSchedule
            {
                Id=workSchedule_ReqvestDto.Id,
                Date=workSchedule_ReqvestDto.Date,
                StartTime=workSchedule_ReqvestDto.StartTime,
                EndTime=workSchedule_ReqvestDto.EndTime,
                BreakDuration=workSchedule_ReqvestDto.BreakDuration,
                UserId=workSchedule_ReqvestDto.UserId
               
            };
    


    var BreakTime= await context.workSchedules.
    FirstOrDefaultAsync(pl => pl.UserId == workSchedule_ReqvestDto.UserId);
     if(BreakTime!=null)
     {
          

     }
      else
     {
       await workScheduleRepository.addBreakTime(pingLog);
       return ;
    }

            
        }
        catch (Exception dbEx)
        {
            throw new Exception("Database error occurred while saving changes.", dbEx.InnerException ?? dbEx);
        }
        
    }

        public Task<WorkSchedule_ResponseDto> GetBreakTime()
        {
            throw new NotImplementedException();
        }
    }
}