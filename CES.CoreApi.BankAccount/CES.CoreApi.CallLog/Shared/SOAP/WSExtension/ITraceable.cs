using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CES.CoreApi.CallLog.Shared.SOAP.WSExtension
{
    /// <summary>
    /// Interface that a proxy class should implement to support tracing
    /// </summary
    public interface ITraceable
    {
        //@@2012-06-18 lb SCR# 665511 Created

        bool IsTraceRequestEnabled { get; set; }
        bool IsTraceResponseEnabled { get; set; }
    }
}
