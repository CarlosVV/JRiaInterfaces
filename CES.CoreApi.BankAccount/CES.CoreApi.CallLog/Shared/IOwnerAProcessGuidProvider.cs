using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.CallLog.Shared
{
    /// <summary>
    /// Provides process-level fixed data for internal operations in DBWsCallLogOps, that must be initialized at the beginning of the application's life
    /// or a task or a call (if the application is of web-service type). However, this value is not maintaned when switching threads as a result of asynchronous operations
    /// or when a new thread is spawn (if this value is stored as a thread-static). To maintain consistency in logging operations, the current SynchronizationContext must be initialized to something that 
    /// implements IOwnerAProcessGuidProvider
    /// </summary>
    public interface IOwnerAProcessGuidProvider
    {
        Guid CurrentGuid { get; set; }
    }
}
