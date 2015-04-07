using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Monitors;

namespace CES.CoreApi.Logging.Factories
{
    public class LogMonitorFactory : Dictionary<string, Func<BaseLogMonitor>>, ILogMonitorFactory
    {
        public T CreateNew<T>() where T : class
        {
            var name = typeof (T).Name;
            return this[name]() as T;
        }
    }
}
