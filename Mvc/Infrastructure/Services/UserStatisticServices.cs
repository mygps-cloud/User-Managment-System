using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Mvc.Infrastructure.Services
{
    public class UserStatisticServices
    {
        public async Task<TimesheetEntry> HourStatistic(UserDto userDto)
        {
            return await Task.Run(() =>
            {
               
                var onlineTimes = userDto?.PingLogDtoResponse?.OnlieTime ?? new List<DateTime>();
                var offlineTimes = userDto?.PingLogDtoResponse?.OflineTime ?? new List<DateTime>();
                
               
                int totalHours = ParseTime(offlineTimes) + ParseTime(onlineTimes);

               
                var startTimes = userDto?.WorkSchedules?.StartTime ?? new List<DateTime>();
                var endTimes = userDto?.WorkSchedules?.EndTime ?? new List<DateTime>();
                TimeSpan totalBreakTime = CalculateTotalBreakTime(startTimes, endTimes);

              
                var timesheetEntry = new TimesheetEntry
                {
                    Date = onlineTimes.FirstOrDefault() != DateTime.MinValue ? onlineTimes.FirstOrDefault() : DateTime.Now,
                    TimeIn = onlineTimes.FirstOrDefault() != DateTime.MinValue ? onlineTimes.FirstOrDefault() : DateTime.Now,
                    TimeOut = offlineTimes.LastOrDefault() != DateTime.MinValue ? offlineTimes.LastOrDefault() : DateTime.Now,
                    Break1Start = startTimes.FirstOrDefault() != DateTime.MinValue ? startTimes.FirstOrDefault() : DateTime.Now,
                    Break1End = endTimes.FirstOrDefault() != DateTime.MinValue ? endTimes.FirstOrDefault() : DateTime.Now,
                    TotalHours = TimeSpan.FromHours(totalHours),
                    TotalProductiveHours = TimeSpan.FromHours(totalHours) - totalBreakTime
                };

                return timesheetEntry;
            });
        }

        private TimeSpan CalculateTotalBreakTime(List<DateTime> startTimes, List<DateTime> endTimes)
        {
            TimeSpan totalBreakTime = TimeSpan.Zero;

           
            int pairCount = Math.Min(startTimes.Count, endTimes.Count);

            for (int i = 0; i < pairCount; i++)
            {
                DateTime startTime = startTimes[i];
                DateTime endTime = endTimes[i];

                if (endTime > startTime)
                {
                    totalBreakTime += endTime - startTime;
                }
            }

            return totalBreakTime;
        }

       private int ParseTime(List<DateTime>? timeList)
{
    int totalMinutes = 0;

   
    if (timeList != null && timeList.Count >= 2)
    {
        for (int i = 0; i < timeList.Count - 1; i += 2)
        {
            DateTime startTime = timeList[i];
            DateTime endTime = timeList[i + 1];

           
            if (endTime > startTime)
            {
                totalMinutes += (int)(endTime - startTime).TotalMinutes;
            }
        }
    }

    
    return totalMinutes / 60;
}

    }
}
