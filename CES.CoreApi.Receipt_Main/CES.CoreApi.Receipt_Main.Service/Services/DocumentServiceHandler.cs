using CES.CoreApi.Receipt_Main.Application.Core.Document;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using CES.CoreApi.Receipt_Main.Service.Jobs;
using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Services
{
    public class DocumentServiceHandler
    {
        private IDocumentService _documentService;
        private readonly ITaxEntityService _taxEntityService;
        private readonly ITaxAddressService _taxAddressService;
        private static ISequenceService _sequenceService;
        private readonly IStoreService _storeService;
        public DocumentServiceHandler()
        {
            _documentService = new CES.CoreApi.Receipt_Main.Application.Core.DocumentService(new DocumentRepository(new ReceiptDbContext()));
            _taxEntityService = new CES.CoreApi.Receipt_Main.Application.Core.TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxAddressService = new CES.CoreApi.Receipt_Main.Application.Core.TaxAddressService(new TaxAddressRepository(new ReceiptDbContext()));
            _sequenceService = new CES.CoreApi.Receipt_Main.Application.Core.SequenceService(new SequenceRepository(new ReceiptDbContext()));
            _storeService = new CES.CoreApi.Receipt_Main.Application.Core.StoreService(new StoreRepository(new ReceiptDbContext()));
        }
        public DocumentServiceHandler(IDocumentService documentService, ITaxEntityService taxEntityService, ITaxAddressService taxAddressService, ISequenceService sequenceService, IStoreService storeService)
        {
            _documentService = documentService;
            _taxEntityService = taxEntityService;
            _taxAddressService = taxAddressService;
            _sequenceService = sequenceService;
            _storeService = storeService;
        }
        internal TaxCreateDocumentResponse CreateDocument(TaxCreateDocumentRequest request)
        {

            throw new NotImplementedException();
        }

        internal object SearchDocument(TaxSearchDocumentRequest r)
        {
            var response = new TaxSearchDocumentResponse();

            var results = null as IEnumerable<DocumentSearchResultItem>;
            response.Results = results.ToList();
            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };
            return response;
        }

        internal object GenerateReceipt(TaxGenerateReceiptRequest request)
        {
            var response = new TaxGenerateReceiptResponse();

            // var document = null as List<Document>;// = _documentrepository.GetByOrderNumberAndFolio(request.OrderNumber, request.Folio);

            //response.Document = document.FirstOrDefault();

            //TODO: Populate all depending objects in the model
            //PopulateDocumentModel(response.Document);

            //TODO: Create PDF for Document Stored
            //CreatePDF(response.Document);


            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };
            return response;
        }

        //private void CreatePDF(Document document)
        //{
        //    throw new NotImplementedException();
        //}

        //private void PopulateDocumentModel(Document document)
        //{
        //    throw new NotImplementedException();
        //}

        internal TaxSIIGetDocumentBatchResponse CreateTaskSiiGetDocumentBatch(TaxSIIGetDocumentBatchRequest taxSIIGetDocumentBatchInternalRequest)
        {
            var response = null as TaxSIIGetDocumentBatchResponse;

            var job = new BatchDownloadJob();
            job.Execute(taxSIIGetDocumentBatchInternalRequest.FolioStart, taxSIIGetDocumentBatchInternalRequest.FolioEnd);

            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };

            return response;
        }

        internal TaxSIIGetDocumentResponse SIIGetDocument(TaxSIIGetDocumentRequest taxSIIGetDocumentInternalRequest)
        {
            var _documentDownloader = new DocumentDownloader();
            var _documentHelper = new DocumentHandlerService(_documentService, _taxEntityService, _taxAddressService, _sequenceService, _storeService);
            var response = new TaxSIIGetDocumentResponse();
            var docType = "39";
            var folio = taxSIIGetDocumentInternalRequest.Folio;
            var respuesta = string.Empty;
            var _parserBoletas = new XmlDocumentParser<EnvioBOLETA> ();
            var indexchunk = 0;
            var acumchunk = 0;

            if (_documentDownloader.RetrieveXML(int.Parse(docType), folio, out respuesta))
            {
                var documentXmlObject = _parserBoletas.GetDocumentObjectFromString(respuesta);

                List<int> detailids = null;
                List<int> docids = null;
                _documentHelper.SaveDocument(folio, folio, ref indexchunk, ref acumchunk, documentXmlObject, ref detailids, ref docids);
            }

            response.Document = _documentService.GetAllDocuments().Where(m => m.Folio == folio).FirstOrDefault();

            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };

            return response;
        }
    }
}