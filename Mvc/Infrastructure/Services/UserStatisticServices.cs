using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Mvc.Infrastructure.Services
{
    public class UserStatisticServices
    {
        public async Task<TimesheetEntry> HourStatistic(UserDto userDto)
        {
            return await Task.Run(() =>
            {
                var onlineTimes = userDto?.PingLogDtoResponse?.OnlineTime ?? new List<DateTime>();
                var offlineTimes = userDto?.PingLogDtoResponse?.OflineTime ?? new List<DateTime>();
                
                TimeSpan totalOnlineTime = CalculateTotalTime(onlineTimes, offlineTimes);

                var startTimes = userDto?.WorkSchedules?.StartTime ?? new List<DateTime>();
                var endTimes = userDto?.WorkSchedules?.EndTime ?? new List<DateTime>();
                TimeSpan totalBreakTime = CalculateTotalBreakTime(startTimes, endTimes);

                var timesheetEntry = new TimesheetEntry
                {
                    Date = onlineTimes.FirstOrDefault() != default ? onlineTimes.First().Date : DateTime.Now.Date,
                    TimeIn = onlineTimes.FirstOrDefault() != default ? onlineTimes.First() : DateTime.MinValue,
                    TimeOut = offlineTimes.LastOrDefault() != default ? offlineTimes.Last() : DateTime.MinValue,
                    BreakStart = startTimes.FirstOrDefault() != default ? startTimes.First() : DateTime.MinValue,
                    BreakEnd = endTimes.FirstOrDefault() != default ? endTimes.First() : DateTime.MinValue,
                    TotalHours = totalOnlineTime,
                    BreakHours = DateTime.MinValue.Add(totalBreakTime),
                    TotalProductiveHours = totalOnlineTime - totalBreakTime
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