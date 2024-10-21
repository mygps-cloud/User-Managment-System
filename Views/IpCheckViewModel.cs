namespace ipstatuschecker;

public class IpCheckViewModel
{
    public string? UserIpAddress { get; set; }=null;
    public string? UserName { get; set; } =null;
    public List<IpStatus>? IpAddresses { get; set; } = null;
}

public class IpStatus
{
    public string? IpAddress { get; set; }=null;
    public string? Status { get; set; }=null;
}

