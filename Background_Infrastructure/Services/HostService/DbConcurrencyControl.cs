

namespace Ipstatuschecker.Background_Infrastructure.Services.HostService
{
    public static class DbConcurrencyControl
    {
        public static readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();
    }

}