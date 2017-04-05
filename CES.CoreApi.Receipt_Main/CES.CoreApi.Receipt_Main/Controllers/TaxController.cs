using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Services;
using CES.CoreApi.Receipt_Main.Utilities;
using CES.CoreApi.Receipt_Main.Validators;
using CES.CoreApi.Receipt_Main.ViewModels;
using CES.CoreApi.Shared.Persistence.Business;
using CES.CoreApi.Shared.Persistence.Data;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CES.CoreApi.Receipt_Main.Controllers
{
    [RoutePrefix("receipt")]
    public class TaxController : ApiController
    {       
        private CAFService _cafservice;
        private DocumentService _docservice;
        private readonly LogService _logService;
        private readonly IPersistenceHelper _persistenceHelper;
        public TaxController()
        {           
            _cafservice = new CAFService();
            _docservice = new DocumentService();
            _logService = new LogService();
            _persistenceHelper = new PersistenceHelper(new PersistenceRepository());
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
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxCreateCAFRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxCreateCAFInternalRequest = AutoMapper.Mapper.Map<TaxCreateCAFRequest>(request);

            //taxCreateCAFInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");


            var serviceResponse = _cafservice.CreateCAF(taxCreateCAFInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxCreateCAFResponseViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxCreateCAFResponseViewModel>(response, 0, 0, PersistenceEventType.TaxCreateCAFResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }


        //Search per Type, RNGD, RNGH
        [HttpPost]
        [Route("tax/caf/search")]
        public IHttpActionResult PostSearchCAFByType(ServiceTaxSearchCAFByTypeRequestViewModel request)
        {
            #region Persistence
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxSearchCAFByTypeRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxSearchCAFByTypeInternalRequest = AutoMapper.Mapper.Map<TaxSearchCAFByTypeRequest>(request);

            //taxSearchCAFByTypeInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.SearchCaf(taxSearchCAFByTypeInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxSearchCAFByTypeResponseViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxSearchCAFByTypeResponseViewModel>(response, 0, 0, PersistenceEventType.TaxSearchCAFResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //-	Update
        [HttpPut]
        [Route("tax/caf")]
        public IHttpActionResult UpdateCAF(ServiceTaxUpdateCAFRequestViewModel request)
        {
            #region Persistence
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxUpdateCAFRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxUpdateCAFInternalRequest = AutoMapper.Mapper.Map<TaxUpdateCAFRequest>(request);

            //taxUpdateCAFInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.UpdateCAF(taxUpdateCAFInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxUpdateCAFResponseViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxUpdateCAFResponseViewModel>(response, 0, 0, PersistenceEventType.TaxUpdateCAFResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //Delete   
        [HttpDelete]
        [Route("tax/caf")]
        public IHttpActionResult DeleteCAF(ServiceTaxDeleteCAFRequestViewModel request)
        {
            #region Persistence
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxDeleteCAFRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxDeleteCAFInternalRequest = AutoMapper.Mapper.Map<TaxDeleteCAFRequest>(request);

            //taxDeleteCAFInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.DeleteCAF(taxDeleteCAFInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxDeleteCAFResponseViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxDeleteCAFRequestViewModel>(response, 0, 0, PersistenceEventType.TaxDeleteCAFResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //GetFolio
        [HttpPost]
        [Route("tax/caf/folio")]
        public IHttpActionResult PostGetFolio(ServiceTaxGetFolioRequestViewModel request)
        {
            #region Persistence
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxGetFolioRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxGetFolioInternalRequest = AutoMapper.Mapper.Map<TaxGetFolioRequest>(request);

            //taxGetFolioInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.GetFolio(taxGetFolioInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxGetFolioResponseViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxGetFolioResponseViewModel>(response, 0, 0, PersistenceEventType.TaxGetFolioResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //UpdateFolio
        [HttpPatch]
        [Route("tax/caf/folio")]
        public IHttpActionResult UpdateFolio(ServiceTaxUpdateFolioRequestViewModel request)
        {
            #region Persistence
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxUpdateFolioRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxUpdateFolioInternalRequest = AutoMapper.Mapper.Map<TaxUpdateFolioRequest>(request);

            //taxUpdateFolioInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");
            var serviceResponse = _cafservice.UpdateFolio(taxUpdateFolioInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxUpdateFolioResponseViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxUpdateFolioResponseViewModel>(response, 0, 0, PersistenceEventType.TaxUpdateFolioResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }

        //Create
        [HttpPost]
        [Route("tax/document")]
        public IHttpActionResult CreateDocument(ServiceTaxCreateDocumentRequestViewModel request)
        {
            #region Persistence
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxCreateDocumentRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxCreateDocumentInternalRequest = AutoMapper.Mapper.Map<TaxCreateDocumentRequest>(request);

            //taxCreateDocumentInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");
            var serviceResponse = _docservice.CreateDocument(taxCreateDocumentInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxCreateDocumentResponseViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxCreateDocumentResponseViewModel>(response, 0, 0, PersistenceEventType.TaxCreateDocumentResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
        //Search por tipo, rango fecha, folio, orderNo, StoreId, IdType y IdNumber, Emisor/Receptor
        [HttpPost]
        [Route("tax/document/search")]
        public IHttpActionResult SearchDocument(ServiceTaxSearchDocumentRequestViewModel request)
        {
            #region Persistence
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxSearchDocumentRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxSearchDocumentInternalRequest = AutoMapper.Mapper.Map<TaxSearchDocumentRequest>(request);

            //taxGenerateInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");
            var serviceResponse = _docservice.SearchDocument(taxSearchDocumentInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxSearchDocumentResponseViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxSearchDocumentResponseViewModel>(response, 0, 0, PersistenceEventType.TaxSearchDocumentResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
        //-	GenerateReceipt
        [HttpPost]
        [Route("tax/generate")]
        public IHttpActionResult GenerateReceipt(ServiceTaxGenerateReceiptRequestViewModel request)
        {
            #region Persistence
            //var client = new Client();
            //request.PersistenceID = client.GetPersistenceID();           
            #endregion

            _logService.LogInfoObjectToJson("Request", request);
            Logging.Log.Info("Generating request...");

            var results = new ServiceTaxGenerateReceiptRequestViewModelValidator().Validate(request);

            if (!results.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, results);
            }

            var taxGenerateReceiptInternalRequest = AutoMapper.Mapper.Map<TaxGenerateReceiptRequest>(request);

            //taxGenerateReceiptInternalRequest.HeaderInfo = new HeaderInfo
            //{
            //    ApplicationId = HeaderHelper.ApplicationId,
            //    CesUserId = HeaderHelper.CesUserId,
            //    CesAppObjectId = HeaderHelper.CesAppObjectId,
            //    CesRequestTime = HeaderHelper.CesRequestTime,
            //};

            Logging.Log.Info("Processing call...");
            var serviceResponse = _docservice.GenerateReceipt(taxGenerateReceiptInternalRequest);
            Logging.Log.Info("Processed Successfully.");

            Logging.Log.Info("Returning Response.");

            var response = AutoMapper.Mapper.Map<ServiceTaxGenerateReceiptRequestViewModel>(serviceResponse);

            #region Persistence
            _persistenceHelper.CreatePersistence<ServiceTaxGenerateReceiptRequestViewModel>(response, 0, 0, PersistenceEventType.TaxGenerateReceiptResponse);
            #endregion

            return Content(HttpStatusCode.OK, response);
        }
        //-	SIISendDocument
        [HttpPost]
        [Route("tax/sii/document")]
        public IHttpActionResult SIISendDocument(ServiceTaxSIISendDocumentRequestViewModel request)
        {
            throw new NotImplementedException();
        }
        //-	SIIGetDocument
        [HttpPost]
        [Route("tax/sii/document/get")]
        public IHttpActionResult SIIGetDocument(ServiceTaxSIIGetDocumentRequestViewModel request)
        {
            throw new NotImplementedException();
        }
    }
}
