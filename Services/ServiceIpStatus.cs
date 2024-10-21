using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;
using Ipstatuschecker.interfaces;
using Ipstatuschecker.Interfaces;

namespace Ipstatuschecker.Services
{
    public class ServiceIpStatus(IQueryIpStatusRepository<User> qeuryIpStatusRepository,
    ICommandIpStatusRepository<User> commandIpStatusRepository) : Iservices<UserDto>
    {
        public async Task<bool> AddNewUser(UserDto userDto)
{
    var user = new User
    {
        Id = userDto.Id,
        Name = userDto.Name,
        IpStatuses = userDto.IpStatuses?.Select(ip => new IpStatus
        {
            Id = ip.Id,
            IpAddress = ip.IpAddress,
            Status = ip.Status,
            UserId = userDto.Id
        }).ToList(),
        Devices = userDto.Devices?.Select(device => new Device
        {
            Id = device.Id,
            DeviceNames = device.DeviceNames,
            UserId = userDto.Id
        }).ToList()
    };

    try
    {
        await commandIpStatusRepository.CreateUser(user);
        return true;
    }
    catch (Exception)
    {
        return false;
    }
}

        public Task<bool> DelteUserById(UserDto entety)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDto>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetByUserNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateNewUser(UserDto entety)
        {
            throw new NotImplementedException();
        }
    }
}