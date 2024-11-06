using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface  IPstatusService
    {
          Task<List<IpStatusDto>> GetAllUsersWitchIp();
    }
}