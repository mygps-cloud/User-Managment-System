namespace ipstatuschecker.Background_Infrastructure.Services.TimeControlServices.Result;
public record CheckInOutServiceResult
    {
        public bool HasOnlineRecordForToday { get; init; }
        public bool HasSufficientTimePassed { get; init; }
        public bool HasOfflineRecordForToday { get; init; }
        public bool LastTimeIn { get; init; }
    }
