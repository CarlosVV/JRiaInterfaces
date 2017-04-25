using CES.CoreApi.Receipt_Main.Filters.Responses;
using CES.CoreApi.Receipt_Main.Services;
using CES.CoreApi.Shared.Persistence.Business;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.Security.CoreApi.Models;
using System.Net.Http;
using System.Web;
using System.Linq;
using CES.CoreApi.Shared.Persistence.Data;
using Newtonsoft.Json;
using System;
using CES.CoreApi.Shared.Persistence.Model;
using System.Collections.Generic;
using System.Net.Http.Headers;
using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.ViewModels;
using System.Web.Http.Controllers;

namespace CES.CoreApi.Receipt_Main.Utilities
{
    /// <summary>
    /// Client  Identity: Don't change it.
    /// To get applicationID, application session and other info from header 
    /// </summary>
    public class Client
    {
        private readonly IPersistenceHelper _persistenceHelper;
        private readonly LogService _logService;
        private readonly HttpRequestMessage _request;
        public Client()
        {
            _persistenceHelper = new PersistenceHelper(new PersistenceRepository());
            _logService = new LogService();
            _request = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
        }
        public string GetCorrelationId(HttpRequestMessage request)
        {
            var correlationIds = null as System.Collections.Generic.IEnumerable<string>;
            if (request.Headers.TryGetValues("CorrelationId", out correlationIds))
            {
                var key = string.Join("", correlationIds);
                if (!string.IsNullOrEmpty(key))
                    return key;
            }
            return request.GetCorrelationId().ToString();
        }     

        public long GetPersistenceID()
        {
            bool isValid = true;
            ErrorResponse errorResponse = null;

            return GetPersistenceID(out isValid, out errorResponse);
        }
        public long GetPersistenceID(out bool isValid, out ErrorResponse errorResponse)
        {
            isValid = true;
            errorResponse = null;
            long persistenceID = 0;
            var request = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;

            var headers = request!=null? request.Headers : null as HttpRequestHeaders;
            if (headers != null && headers.Contains("persistenceID"))
            {
                string peristenceID = headers.GetValues("persistenceID").First();

                return int.Parse(peristenceID);
            }

            var jsonContent = request.Content.ReadAsStringAsync().Result;

            try
            {
                if (request.Method.Method.Equals("Post")  && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/caf"))
                {

                    var serviceTaxCreateCAFRequestViewModel = JsonConvert.DeserializeObject<ServiceTaxCreateCAFRequestViewModel>(jsonContent);


                    if (serviceTaxCreateCAFRequestViewModel != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxCreateCAFRequestViewModel>(serviceTaxCreateCAFRequestViewModel, 0, PersistenceEventType.TaxCreateCAFRequest);

                        persistenceID = persistence.PersistenceID;
                    }


                }

                request.Headers.Add("persistenceID", persistenceID.ToString());

                return persistenceID;

            }
            catch (Exception ex)
            {
                isValid = false;
                persistenceID = _persistenceHelper.CreatePersistence<string>(jsonContent, 0, 0, PersistenceEventType.InvalidRequestModel).PersistenceID;

                _logService.LogInfo($"Exception: Invalid Request Model. {jsonContent}");
                var errors = new List<Error>() { new Error() { Message = ex.Message, Property = "", Code = 400 } };
                errorResponse = new ErrorResponse("The request is not valid, possibly it is not well formed", (int)System.Net.HttpStatusCode.BadRequest, errors, request.GetCorrelationId(), persistenceID);

                _persistenceHelper.CreatePersistence<ErrorResponse>(errorResponse, persistenceID, 0, PersistenceEventType.ErrorResponse);

                _logService.LogInfoObjectToJson("Error Response: ", errorResponse);
                return persistenceID;
            }
        }

        public static ClientApplicationIdentity Identity
        {
            get
            {
                return System.Threading.Thread.CurrentPrincipal.Identity as ClientApplicationIdentity;
            }
        }

        public static object GetCorrelationId2(HttpRequestMessage request)
        {
            var correlationIds = null as System.Collections.Generic.IEnumerable<string>;
            if (request.Headers.TryGetValues("CorrelationId", out correlationIds))
            {
                var key = string.Join("", correlationIds);
                if (!string.IsNullOrEmpty(key))
                    return key;
            }
            return request.GetCorrelationId();
        }

        public string ApplicationId
        {
            get
            {
                IEnumerable<string> values;
                if(_request.Headers.TryGetValues("ApplicationId", out values))
                {
                    return values.FirstOrDefault();
                }

                return string.Empty;
            }

        }

        public string CesAppObjectId
        {
           
            get
            {
                IEnumerable<string> values;
                if (_request.Headers.TryGetValues("ces-appObjectId", out values))
                {
                    return values.FirstOrDefault();
                }

                return string.Empty;                
            }
        }

        public string CesUserId
        {
            get
            {
                IEnumerable<string> values;
                if (_request.Headers.TryGetValues("ces-userId", out values))
                {
                    return values.FirstOrDefault();
                }

                return string.Empty;

            }
        }     

        public string CesRequestTime
        {
            get
            {
                IEnumerable<string> values;
                if (_request.Headers.TryGetValues("ces-requestTime", out values))
                {
                    return values.FirstOrDefault();
                }

                return string.Empty;
            }
        }
    }
}