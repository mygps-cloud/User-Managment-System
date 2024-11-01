
namespace Ipstatuschecker.Background_Infrastructure.Services
{
    public class TimeControlService
    {
        private readonly DateTime startTime;

        public TimeControlService(DateTime startTime)
        {
            this.startTime = startTime;
        }

        public bool IsTenSecondsPassed()
        {
            
            return DateTime.Now >= startTime.AddSeconds(10);
        }

    }
}
