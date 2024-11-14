
using System.Net.NetworkInformation;


namespace Ipstatuschecker.Background_Infrastructure.Services.HostService
{
    public class PingIpChecker
    {
        private readonly ILogger<PingIpChecker> logger;

        public PingIpChecker(ILogger<PingIpChecker> logger)
        {
            this.logger = logger;
        }

        public async Task<bool> PingIp(string ipAddress)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(ipAddress);
                    logger.LogInformation($"Pinged {ipAddress}, Status: {reply.Status}, Roundtrip Time: {reply.RoundtripTime} ms");

                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error pinging {ipAddress}: {ex.Message}");
                return false;
            }
        }
    }
}
