using Ipstatuschecker.Abstractions.interfaces.IServices;
using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Dto
{
   public class PingLogDtoReqvest:IUserEntity
{
   
     public int Id { get; set; }
    public List<DateTime>? OnlineTime { get; set; }
    public  List<DateTime>?OflineTime { get; set; }
    public int? UserId { get; set; }
    public User? User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

}