using System;

namespace CES.CoreApi.Foundation.Configuration
{
    public class ApplicationConfiguration
    {
        public TimeSpan CacheLifetime { get { return default(TimeSpan); } }
        public string CacheName { get { return null; } }
    }
}
