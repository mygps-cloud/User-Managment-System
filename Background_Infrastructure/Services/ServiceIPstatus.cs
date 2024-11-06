
using Ipstatuschecker.Abstractions.interfaces.IRepository;
using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.Dto;



namespace Background_Infrastructure.Services
{public class ServiceIPstatus : IPstatusService
{
    private readonly IPstatusRepository _pstatusRepository;

    public ServiceIPstatus(IPstatusRepository pstatusRepository)
    {
        _pstatusRepository = pstatusRepository;
    }

    public async Task<List<IpStatusDto>> GetAllUsersWitchIp()
    {
        var all_Ip = await _pstatusRepository.GetallIpStatus();

        var usersIp = all_Ip.Select(ip => new IpStatusDto
        {
            Id = ip.Id,
            IpAddress = ip.IpAddress,
            Status = ip.Status
        }).ToList();

        return usersIp;
    }
}

}
