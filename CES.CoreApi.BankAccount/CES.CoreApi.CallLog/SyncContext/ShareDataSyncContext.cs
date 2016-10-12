using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CES.CoreApi.CallLog.SyncContext
{
    /// <summary>
    /// This SynchronizationContext's implementation is automatically maintained for asynchronous operations
    /// </summary>
    public class ShareDataSyncContext: SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            SynchronizationContext.SetSynchronizationContext(this);

            this.Send(d, state);
        }
    }
}
