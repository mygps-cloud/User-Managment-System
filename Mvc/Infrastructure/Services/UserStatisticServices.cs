

using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Mvc.Infrastructure.Services
{
    public class UserStatisticServices
    {
        public async Task<List<TimesheetEntry>> HourStatistics(UserDto userDto)
        {
            return await Task.Run(() =>
            {
               
                var onlineTimes = userDto?.PingLogDtoResponse?.OnlineTime?.ToList() ?? new List<DateTime> { };
                var offlineTimes = userDto?.PingLogDtoResponse?.OflineTime?.ToList() ?? new List<DateTime>();

         
                var startTimes = userDto?.WorkSchedules?.StartTime?.ToList() ?? new List<DateTime> { };
                var endTimes = userDto?.WorkSchedules?.EndTime?.ToList() ?? new List<DateTime> { };

              
                int totalEntries = Math.Min(onlineTimes.Count, offlineTimes.Count);
                List<TimesheetEntry> timesheetEntries = new List<TimesheetEntry>();

                for (int i = 0; i < totalEntries; i++)
                {
                    DateTime timeIn = onlineTimes[i];
                    DateTime timeOut = offlineTimes[i];

                 
                    TimeSpan totalOnlineTime = CalculateTotalTime(new List<DateTime> { timeIn }, new List<DateTime> { timeOut });

                    TimeSpan totalBreakTime = TimeSpan.Zero;
                    if (i < startTimes.Count && i < endTimes.Count)
                    {
                        totalBreakTime = CalculateTotalBreakTime(new List<DateTime> { startTimes[i] }, new List<DateTime> { endTimes[i] });
                    }

                 
                    var timesheetEntry = new TimesheetEntry
                    {
                        Date = timeIn != default ? timeIn.Date : DateTime.Now.Date,
                        TimeIn = timeIn != default ? timeIn : DateTime.MinValue,
                        TimeOut = timeOut != default ? timeOut : DateTime.MinValue,
                        BreakStart = (i < startTimes.Count && startTimes[i] != default) ? startTimes[i] : DateTime.MinValue,
                        BreakEnd = (i < endTimes.Count && endTimes[i] != default) ? endTimes[i] : DateTime.MinValue,
                        TotalHours = totalOnlineTime,
                        BreakHours = DateTime.MinValue.Add(totalBreakTime),
                        TotalProductiveHours = totalOnlineTime - totalBreakTime
                    };

                    timesheetEntries.Add(timesheetEntry);
                }

                return timesheetEntries;
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

   
        private TimeSpan CalculateTotalTime(List<DateTime> onlineTimes, List<DateTime> offlineTimes)
        {
            TimeSpan totalTime = TimeSpan.Zero;

            int pairCount = Math.Min(onlineTimes.Count, offlineTimes.Count);
            for (int i = 0; i < pairCount; i++)
            {
                DateTime onlineTime = onlineTimes[i];
                DateTime offlineTime = offlineTimes[i];

                if (offlineTime > onlineTime)
                {
                    totalTime += offlineTime - onlineTime;
                }
            }

            return totalTime;
        }
    }
}