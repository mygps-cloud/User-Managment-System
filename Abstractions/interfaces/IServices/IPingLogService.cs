using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IPingLogService
    {
          Task<bool>  addPingLogService(PingLogDtoReqvest pingLogDtoReqvest);
          Task<bool> addworkScheduleService(PingLogDtoReqvest  pingLogDtoReqvest);
          Task<List<PingLogDtoResponse>> GetAll();
    }
}