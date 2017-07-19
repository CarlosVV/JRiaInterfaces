using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Service.Services;
using CES.CoreApi.Receipt_Main.Service.Utilities;
using CES.CoreApi.Receipt_Main.Service.Validators;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Net;
using System.Web.Http;

namespace CES.CoreApi.Receipt_Main.Service.Controllers
{
    [RoutePrefix("receipt")]
    public class TaxController : ApiController
    {        
        private CafServiceHandler _cafservice;
        private DocumentServiceHandler _docservice;
        private readonly LogService _logService;
        private IPersistenceHelper _persistenceHelper;
        private IClient _client;
        public TaxController(ICafService cafdomain)
        {
            _cafservice = new CafServiceHandler(cafdomain);
            _docservice = new  DocumentServiceHandler();
            _logService = new LogService();
            _persistenceHelper = PersistenceHelperFactory.GetPersistenceHelper();
            _client = ClientFactory.GetClient();
        }
        [HttpGet]
        [Route("Ping")]
        public IHttpActionResult PingServer()
        {
            return Content(HttpStatusCode.OK, $"Hello CES.CoreApi.Receipt_Main TaxGenerate Service! {System.DateTime.UtcNow}");
        }              

        [HttpPost]
        [Route("tax/caf")]
        public IHttpActionResult PostCreateCAF(ServiceTaxCreateCAFRequestViewModel request)
        {
            #region Persistence            
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxCreateCAFRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxCreateCAFInternalRequest = AutoMapper.Mapper.Map<TaxCreateCafRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.CreateCAF(taxCreateCAFInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");
            var response = AutoMapper.Mapper.Map<ServiceTaxCreateCAFResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxCreateCAFResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxCreateCAFResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
        
        //-	Update
        [HttpPut]
        [Route("tax/caf")]
        public IHttpActionResult UpdateCAF(ServiceTaxUpdateCAFRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxUpdateCAFRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxUpdateCAFInternalRequest = AutoMapper.Mapper.Map<TaxUpdateCafRequest>(request);

            taxUpdateCAFInternalRequest.HeaderInfo = new HeaderInfo
            {
                ApplicationId = HeaderHelper.ApplicationId,
                CesUserId = HeaderHelper.CesUserId,
                CesAppObjectId = HeaderHelper.CesAppObjectId,
                CesRequestTime = HeaderHelper.CesRequestTime,
            };

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.UpdateCAF(taxUpdateCAFInternalRequest);
            

            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxUpdateCAFResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;
            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxUpdateCAFResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxUpdateCAFResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //Search per Type, RNGD, RNGH
        [HttpPost]
        [Route("tax/caf/search")]
        public IHttpActionResult PostSearchCAFByType(ServiceTaxSearchCAFByTypeRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxSearchCAFByTypeRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxSearchCAFByTypeInternalRequest = AutoMapper.Mapper.Map<TaxSearchCAFByTypeRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.SearchCaf(taxSearchCAFByTypeInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxSearchCAFByTypeResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxSearchCAFByTypeResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxSearchCAFResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
        
        //Delete   
        //[HttpDelete]
        [HttpPost]
        [Route("tax/caf/delete")]
        public IHttpActionResult DeleteCAF(ServiceTaxDeleteCAFRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxDeleteCAFRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxDeleteCAFInternalRequest = AutoMapper.Mapper.Map<TaxDeleteCAFRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.DeleteCAF(taxDeleteCAFInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxDeleteCAFResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;
            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxDeleteCAFResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxDeleteCAFResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //GetFolio
        [HttpPost]
        [Route("tax/caf/folio")]
        public IHttpActionResult PostGetFolio(ServiceTaxGetFolioRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxGetFolioRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxGetFolioInternalRequest = AutoMapper.Mapper.Map<TaxGetFolioRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.GetFolio(taxGetFolioInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxGetFolioResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;
            
            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxGetFolioResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxGetFolioResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //UpdateFolio
        [HttpPatch]
        [Route("tax/caf/folio")]
        public IHttpActionResult UpdateFolio(ServiceTaxUpdateFolioRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxUpdateFolioRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxUpdateFolioInternalRequest = AutoMapper.Mapper.Map<TaxUpdateFolioRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.UpdateFolio(taxUpdateFolioInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxUpdateFolioResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;
            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxUpdateFolioResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxUpdateFolioResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //Create
        [HttpPost]
        [Route("tax/document")]
        public IHttpActionResult CreateDocument(ServiceTaxCreateDocumentRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxCreateDocumentRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxCreateDocumentInternalRequest = AutoMapper.Mapper.Map<TaxCreateDocumentRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _docservice.CreateDocument(taxCreateDocumentInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxCreateDocumentResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;
            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxCreateDocumentResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxCreateDocumentResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
        //Search por tipo, rango fecha, folio, orderNo, StoreId, IdType y IdNumber, Emisor/Receptor
        [HttpPost]
        [Route("tax/document/search")]
        public IHttpActionResult SearchDocument(ServiceTaxSearchDocumentRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxSearchDocumentRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxSearchDocumentInternalRequest = AutoMapper.Mapper.Map<TaxSearchDocumentRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _docservice.SearchDocument(taxSearchDocumentInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxSearchDocumentResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxSearchDocumentResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxSearchDocumentResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
        //-	GenerateReceipt
        [HttpPost]
        [Route("tax/generate")]
        public IHttpActionResult GenerateReceipt(ServiceTaxGenerateReceiptRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxGenerateReceiptRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxGenerateReceiptInternalRequest = AutoMapper.Mapper.Map<TaxGenerateReceiptRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _docservice.GenerateReceipt(taxGenerateReceiptInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxGenerateReceiptRequestViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxGenerateReceiptRequestViewModel>(response, persistenceID, 0, PersistenceEventType.TaxGenerateReceiptResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //-	SIISendDocument
        [HttpPost]
        [Route("tax/sii/document")]
        public IHttpActionResult SIISendDocument(ServiceTaxSIISendDocumentRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            throw new NotImplementedException();
        }

        //-	SIIGetDocument
        [HttpPost]
        [Route("tax/sii/document/get")]
        public IHttpActionResult SIIGetDocument(ServiceTaxSIIGetDocumentRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxSIIGetDocumentRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxSIIGetDocumentInternalRequest = AutoMapper.Mapper.Map<TaxSIIGetDocumentRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _docservice.SIIGetDocument(taxSIIGetDocumentInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxSIIGetDocumentResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxSIIGetDocumentResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxSIIGetDocumentBatchResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
        //Get Document Batch from SII
        [HttpPost]
        [Route("tax/sii/documentbatch/get")]
        public IHttpActionResult SIIGetDocumentBatch(ServiceTaxSIIGetDocumentBatchRequestViewModel request)
        {
            #region Persistence          
            var persistenceID = _client.GetPersistenceID();
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxSIIGetDocumentBatchRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxSIIGetDocumentBatchInternalRequest = AutoMapper.Mapper.Map<TaxSIIGetDocumentBatchRequest>(request);

            Logging.Log.Info("Processing call...");
            var serviceResponse = _docservice.CreateTaskSiiGetDocumentBatch(taxSIIGetDocumentBatchInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxSIIGetDocumentBatchResponseViewModel>(serviceResponse);
            response.PersistenceId = persistenceID;

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxSIIGetDocumentBatchResponseViewModel>(response, persistenceID, 0, PersistenceEventType.TaxSIIGetDocumentBatchResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
    }
}
