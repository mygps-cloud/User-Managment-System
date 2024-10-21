using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace ipstatuschecker
{
    public class PingService
    {
        public async Task<bool> PingIp(string ipAddress)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(ipAddress);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception ex)
            {
                // აქ შეგიძლიათ შეცდომის ლოგირება ან გამოტანა
                Console.WriteLine($"Ping error for {ipAddress}: {ex.Message}");
                return false;
            }
        }
    }
}
