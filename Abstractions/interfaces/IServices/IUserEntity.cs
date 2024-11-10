

using Ipstatuschecker.DomainEntity;

namespace Ipstatuschecker.Abstractions.interfaces.IServices
{
    public interface IUserEntity
    {
   public int Id { get; set; }
    public List<DateTime>? OnlineTime { get; set; }
    public  List<DateTime>?OflineTime { get; set; }
    public int? UserId { get; set; } 
    public User? User { get; set; }
    }

}