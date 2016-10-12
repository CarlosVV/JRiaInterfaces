using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CES.CoreApi.CallLog.Shared
{
    /// <summary>
    /// Type of a traced message
    /// </summary
    public enum DataFlowDirection
    {
        //@@2014-09-10 lb SCR# 2124211 Created (SCR used to allocated this change)
        //@@2015-02-06 lb SCR# 2235011 Enhanced logging 
        Request = 1,
        Response = 2,
    }
}
