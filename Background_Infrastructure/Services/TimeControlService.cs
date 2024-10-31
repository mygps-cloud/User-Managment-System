namespace Ipstatuschecker.Background_Infrastructure.Services
{
    public class TimeControlService
    {
        private bool _isWithinTimeFrame;

        public TimeControlService()
        {
            Task.Run(() => UpdateTimeFrameStatus());
        }

        public bool IsWithinTimeFrame => _isWithinTimeFrame;

        private async Task UpdateTimeFrameStatus()
        {
            var startTime = new TimeSpan(9, 0, 0);
            var endTime = startTime.Add(new TimeSpan(6, 0, 0)); 
            
            var checkInterval = TimeSpan.FromMilliseconds(1000); 
            
            while (true)
            {
                var currentTime = DateTime.Now.TimeOfDay;
                _isWithinTimeFrame = currentTime >= startTime && currentTime < endTime;
                
                await Task.Delay(checkInterval); 
            }
        }
    }
}
