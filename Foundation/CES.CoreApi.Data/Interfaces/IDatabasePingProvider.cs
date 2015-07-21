using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Foundation.Data.Interfaces
{
    public interface IDatabasePingProvider
    {
        PingResponseModel PingDatabases();
    }
}