using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Services
{
    public class Log4NetProxy
    {
        public void PublishInformation(string message)
        {
            Logging.Log.Info(message);
        }

        public void PublishError(string message)
        {
            Logging.Log.Error(message);
        }

        public void PublishWarning(string message)
        {
            Logging.Log.Warning(message);
        }
    }
}