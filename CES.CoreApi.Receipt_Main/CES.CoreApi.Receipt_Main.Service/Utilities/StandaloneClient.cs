using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CES.CoreApi.Receipt_Main.Service.Filters.Responses;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public class StandaloneClient : IClient
    {
        public string ApplicationId
        {
            get
            {
                return string.Empty;
            }
        }

        public string CesAppObjectId
        {
            get
            {
                return string.Empty;
            }
        }

        public string CesRequestTime
        {
            get
            {
                return string.Empty;
            }
        }

        public string CesUserId
        {
            get
            {
                return string.Empty;
            }
        }

        public string GetCorrelationId(HttpRequestMessage request)
        {
            return string.Empty;
        }

        public long GetPersistenceID()
        {
            return 0L;
        }

        public long GetPersistenceID(out bool isValid, out ErrorResponse errorResponse)
        {
            isValid = true;
            errorResponse = null;
            return 0L;
        }
    }
}