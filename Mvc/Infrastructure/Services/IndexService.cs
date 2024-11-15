using Abstractions.interfaces.Iservices;
using Ipstatuschecker.Background_Infrastructure.Services.HostService;
using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Mvc.Infrastructure.Services
{
    public class IndexService(IUserservices<UserDto> iservices, PingIpChecker pingIpChecker)
    {
         public async Task<IEnumerable<UserDto>> Dai()
        {
            var users = await iservices.GetAllUsers();

            if (users == null || !users.Any())
            {
                return Enumerable.Empty<UserDto>();
            }

            var tasks = users.Select(async user =>
            {
                if (user == null)
                {
                    return null;
                }

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name ?? string.Empty,
                    IpStatuses = new List<IpStatusDto>(),
                    Devices = new List<DeviceDto>()
                };

                if (user.IpStatuses != null)
                {
                    foreach (var ipStatus in user.IpStatuses)
                    {
                        if (ipStatus != null)
                        {
                            string ipAddress = ipStatus.IpAddress ?? string.Empty;

                            string status = await pingIpChecker.PingIp(ipAddress) ? "Online" : "Offline";

                            userDto.IpStatuses.Add(new IpStatusDto
                            {
                                IpAddress = ipAddress,
                                Status = status
                            });
                        }
                    }
                }

                if (user.Devices != null)
                {
                    foreach (var device in user.Devices)
                    {
                        if (device != null)
                        {
                            userDto.Devices.Add(new DeviceDto
                            {
                                DeviceNames = device.DeviceNames ?? string.Empty
                            });
                        }
                    }
                }

                return userDto;
            });

            var userDtos = await Task.WhenAll(tasks);
            return userDtos.Where(dto => dto != null)!;
        }
        
    }
}