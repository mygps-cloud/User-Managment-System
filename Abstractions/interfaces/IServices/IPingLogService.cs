using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IPingLogService
    {
          Task<bool>  addTimeInService(PingLogDtoReqvest pingLogDtoReqvest,bool status);
          Task<List<PingLogDtoResponse>> GetAll();
    }
}