using System;

namespace CES.CoreApi.Common.Enumerations
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
