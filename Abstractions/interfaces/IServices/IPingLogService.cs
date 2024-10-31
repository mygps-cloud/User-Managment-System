using Ipstatuschecker.Dto;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IPingLogService
    {
          Task<bool>  addService(PingLogDtoReqvest pingLogDtoReqvest);
          Task<List<PingLogDtoResponse>> GetAll();
    }
}