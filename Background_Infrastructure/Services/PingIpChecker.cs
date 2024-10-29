
using System.Net.NetworkInformation;

namespace Ipstatuschecker.Background_Infrastructure.Services
{
    public class PingIpChecker(ILogger<PingIpChecker> logger)
    {
         public async Task<bool> PingIp(string ipAddress)
{
    try
    {
        using (var ping = new Ping())
        {
            var reply = await ping.SendPingAsync(ipAddress);
            logger.LogInformation($"Pinged {ipAddress}, Status: {reply.Status}, Roundtrip Time: {reply.RoundtripTime} ms");
            
                return reply.Status == IPStatus.Success? true :false;
            
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