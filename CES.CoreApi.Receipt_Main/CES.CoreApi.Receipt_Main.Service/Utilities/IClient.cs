using System.Net.Http;
using CES.CoreApi.Receipt_Main.Service.Filters.Responses;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public interface IClient
    {
        string ApplicationId { get; }
        string CesAppObjectId { get; }
        string CesRequestTime { get; }
        string CesUserId { get; }

        string GetCorrelationId(HttpRequestMessage request);
        long GetPersistenceID();
        long GetPersistenceID(out bool isValid, out ErrorResponse errorResponse);
    }
}