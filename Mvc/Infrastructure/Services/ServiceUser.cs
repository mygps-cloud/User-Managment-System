using Abstractions.interfaces;
using Abstractions.interfaces.IRepository;
using Abstractions.interfaces.Iservices;
using Ipstatuschecker.DomainEntity;
using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Mvc.Infrastructure.Services
{
    public class ServiceUser(IQueryUserRepository<User> qeuryIpStatusRepository,
    ICommandUserRepository<User> commandIpStatusRepository) : IUserservices<UserDto>
    {
        public async Task<bool> AddNewUser(UserDto userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                IpStatuses = (userDto.IpStatuses ?? new List<IpStatusDto>()).Select(ip => new IpStatus
                {
                    Id = ip.Id,
                    IpAddress = ip.IpAddress ?? string.Empty,
                    Status = ip.Status ?? string.Empty
                }).ToList(),
                Devices = (userDto.Devices ?? new List<DeviceDto>()).Select(device => new Device
                {
                    Id = device.Id,
                    DeviceNames = device.DeviceNames ?? string.Empty
                }).ToList()
            };


            try
            {
                await commandIpStatusRepository.Create(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<UserDto> UpdateNewUser(UserDto entity)
        {

            var existingUser = await qeuryIpStatusRepository.GetByIdAsync(entity.Id);

            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }


            existingUser.Name = entity.Name;


            if (entity.IpStatuses != null)
            {

                existingUser.IpStatuses.Clear();
                existingUser.IpStatuses.AddRange(entity.IpStatuses.Select(ip => new IpStatus
                {
                    Id = ip.Id,
                    IpAddress = ip.IpAddress,
                    Status = ip.Status,

                }));
            }

            if (entity.Devices != null)
            {
                existingUser.Devices.Clear();
                existingUser.Devices.AddRange(entity.Devices.Select(device => new Device
                {
                    Id = device.Id,
                    DeviceNames = device.DeviceNames,

                }));
            }


            await commandIpStatusRepository.Update(existingUser);


            return new UserDto
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                IpStatuses = existingUser.IpStatuses.Select(ip => new IpStatusDto
                {
                    Id = ip.Id,
                    IpAddress = ip.IpAddress,
                    Status = ip.Status
                }).ToList(),
                Devices = existingUser.Devices.Select(device => new DeviceDto
                {
                    Id = device.Id,
                    DeviceNames = device.DeviceNames
                }).ToList()
            };
        }
        public async Task<bool> DelteUserById(int entityId)
        {
            try
            {
                bool isDeleted = await commandIpStatusRepository.Delete(entityId);
                if (!isDeleted)
                {
                    throw new Exception("User could not be deleted.");
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }
        //=========================================================================//
        public async Task<List<UserDto>> GetAllUsers()
        {
            try
            {
                var users = await qeuryIpStatusRepository.GetAll();

                var userDtos = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    IpStatuses = user.IpStatuses?.Select(ip => new IpStatusDto
                    {
                        Id = ip.Id,
                        IpAddress = ip.IpAddress,
                        Status = ip.Status
                    }).ToList(),
                    Devices = user.Devices?.Select(device => new DeviceDto
                    {
                        Id = device.Id,
                        DeviceNames = device.DeviceNames
                    }).ToList(),
                    WorkSchedules = user.workSchedule != null ? new WorkSchedule_ResponseDto
                    {
                        StartTime = user.workSchedule.StartTime,
                        EndTime = user.workSchedule.EndTime
                    } : null,
                    PingLogDtoResponse = user.PingLog != null ? new PingLogDtoResponse
                    {
                        Id = user.PingLog.Id,
                        OnlineTime = user.PingLog.OnlineTime,
                        OflineTime = user.PingLog.OflineTime
                    } : null

                }).ToList();

                return userDtos;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An error occurred while retrieving users.", ex);
            }
        }
        public async Task<UserDto> GetByUserIdAsync(int id)
        {

            var user = await qeuryIpStatusRepository.GetByIdAsync(id);


            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }


            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,


                IpStatuses = user.IpStatuses?.Select(ip => new IpStatusDto
                {
                    Id = ip.Id,
                    IpAddress = ip.IpAddress,
                    Status = ip.Status
                }).ToList(),


                Devices = user.Devices?.Select(device => new DeviceDto
                {
                    Id = device.Id,
                    DeviceNames = device.DeviceNames
                }).ToList(),


                PingLogDtoResponse = user.PingLog != null ? new PingLogDtoResponse
                {
                    Id = user.PingLog.Id,
                    OnlineTime = user.PingLog.OnlineTime,
                    OflineTime = user.PingLog.OflineTime
                } : null,


                WorkSchedules = user.workSchedule != null ? new WorkSchedule_ResponseDto
                {
                    Id = user.workSchedule.Id,
                    StartTime = user.workSchedule.StartTime,
                    EndTime = user.workSchedule.EndTime,
                    UserId = user.workSchedule.UserId ?? default(int)

                } : null
            };

            return userDto;
        }

        public async Task<UserDto> GetByUserNameAsync(string name)
        {
            var user = await qeuryIpStatusRepository.GetByNameAsync(name);


            if (user == null)
            {
                throw new Exception("User not found.");
            }


            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                IpStatuses = user.IpStatuses?.Select(ip => new IpStatusDto
                {
                    Id = ip.Id,
                    IpAddress = ip.IpAddress,
                    Status = ip.Status
                }).ToList(),
                Devices = user.Devices?.Select(device => new DeviceDto
                {
                    Id = device.Id,
                    DeviceNames = device.DeviceNames
                }).ToList()
            };

            return userDto;
        }

        public async Task<List<UserDto>> GetUserByIdTolist(int id)
        {
            var user = await qeuryIpStatusRepository.GetByIdAsync(id);


            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }


            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,


                IpStatuses = user.IpStatuses?.Select(ip => new IpStatusDto
                {
                    Id = ip.Id,
                    IpAddress = ip.IpAddress,
                    Status = ip.Status
                }).ToList(),


                Devices = user.Devices?.Select(device => new DeviceDto
                {
                    Id = device.Id,
                    DeviceNames = device.DeviceNames
                }).ToList(),


                PingLogDtoResponse = user.PingLog != null ? new PingLogDtoResponse
                {
                    Id = user.PingLog.Id,
                    OnlineTime = user.PingLog.OnlineTime,
                    OflineTime = user.PingLog.OflineTime
                } : null,


                WorkSchedules = user.workSchedule != null ? new WorkSchedule_ResponseDto
                {
                    Id = user.workSchedule.Id,
                    StartTime = user.workSchedule.StartTime,
                    EndTime = user.workSchedule.EndTime,
                    UserId = user.workSchedule.UserId ?? default(int)

                } : null
            };

            return new List<UserDto> { userDto };
        }
    }
}