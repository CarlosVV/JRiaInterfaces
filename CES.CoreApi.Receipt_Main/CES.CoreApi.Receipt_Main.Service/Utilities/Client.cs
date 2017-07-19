using CES.CoreApi.Receipt_Main.Service.Filters.Responses;
using CES.CoreApi.Receipt_Main.Service.Services;
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
using System.Web.Http.Controllers;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    /// <summary>
    /// Client  Identity: Don't change it.
    /// To get applicationID, application session and other info from header 
    /// </summary>
    public class Client : IClient
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
                if (request.Method.Method.Equals("POST")  && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/caf"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxCreateCAFRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxCreateCAFRequestViewModel>(req, 0, PersistenceEventType.TaxCreateCAFRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/caf"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxUpdateCAFRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxUpdateCAFRequestViewModel>(req, 0, PersistenceEventType.TaxUpdateCAFRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/caf/search"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxSearchCAFByTypeRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxSearchCAFByTypeRequestViewModel>(req, 0, PersistenceEventType.TaxSearchCAFByTypeRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/caf/delete"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxDeleteCAFRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxDeleteCAFRequestViewModel>(req, 0, PersistenceEventType.TaxDeleteCAFRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("PATCH") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/caf/folio"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxUpdateFolioRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxUpdateFolioRequestViewModel>(req, 0, PersistenceEventType.TaxUpdateFolioRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/document"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxCreateDocumentRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxCreateDocumentRequestViewModel>(req, 0, PersistenceEventType.TaxCreateDocumentRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/document/search"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxSearchDocumentRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxSearchDocumentRequestViewModel>(req, 0, PersistenceEventType.TaxSearchDocumentRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/generate"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxGenerateReceiptRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxGenerateReceiptRequestViewModel>(req, 0, PersistenceEventType.TaxGenerateReceiptRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/sii/document"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxSIISendDocumentRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxSIISendDocumentRequestViewModel>(req, 0, PersistenceEventType.TaxSIISendDocumentRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/sii/document/get"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxSIIGetDocumentRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxSIIGetDocumentRequestViewModel>(req, 0, PersistenceEventType.TaxSIIGetDocumentRequest);

                        persistenceID = persistence.PersistenceID;
                    }
                }

                if (request.Method.Method.Equals("POST") && request.RequestUri.AbsoluteUri.ToLower().Contains("/receipt/tax/sii/documentbatch/get"))
                {
                    var req = JsonConvert.DeserializeObject<ServiceTaxSIIGetDocumentBatchRequestViewModel>(jsonContent);

                    if (req != null)
                    {
                        var persistence = _persistenceHelper.CreatePersistenceRequest<ServiceTaxSIIGetDocumentBatchRequestViewModel>(req, 0, PersistenceEventType.TaxSIIGetDocumentBatchRequest);

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