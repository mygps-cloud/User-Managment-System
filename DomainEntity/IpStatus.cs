namespace Ipstatuschecker.DomainEntity
{
   public class IpStatus
{
    public int? Id { get; set; }
    public string? IpAddress { get; set; }
    public string? Status { get; set; }
    
   
    public Device? _Device { get; set; } 

     public int? UserId { get; set; }
    public User? _User { get; set; } 
}

}