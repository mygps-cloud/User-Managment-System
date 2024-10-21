namespace ipstatuschecker;

public class IpCheckViewModel
{
    public string? UserIpAddress { get; set; }
    public string? UserName { get; set; } 
    public List<IpStatus>? IpAddresses { get; set; }
}

public class IpStatus
{
    public string? IpAddress { get; set; }
    public string? Status { get; set; }
}

