namespace Ipstatuschecker.DomainEntity
{
   public class IpStatus
{
    public int? Id { get; set; }
    public string? IpAddress { get; set; }
    public string? Status { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; } 
}

}