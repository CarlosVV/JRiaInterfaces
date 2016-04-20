using System;

namespace CES.CoreApi.Logging.Enumerations
{
    [Flags]
    public enum LogType
    {
        EventLog,
        PerformanceLog,
        ExceptionLog,
        TraceLog,
        DbPerformanceLog,
        SecurityAudit
    }
}
