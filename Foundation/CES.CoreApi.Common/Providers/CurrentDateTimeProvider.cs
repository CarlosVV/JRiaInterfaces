using System;
using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Common.Providers
{
    public class CurrentDateTimeProvider : ICurrentDateTimeProvider
    {
        public DateTime GetCurrentUtc()
        {
            return DateTime.UtcNow;
        }

        public DateTime GetCurrentLocal()
        {
            return DateTime.Now;
        }
    }
}
