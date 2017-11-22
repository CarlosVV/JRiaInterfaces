using CES.CoreApi.Receipt_Main.Application.Core;
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
using Hangfire;
using Newtonsoft.Json;
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
            //_sequenceService = new CES.CoreApi.Receipt_Main.Application.Core.SequenceService(new SequenceRepository(new ReceiptDbContext()));
            _sequenceService = new CES.CoreApi.Receipt_Main.Application.Core.SequenceService();
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
            var response = new TaxCreateDocumentResponse();
            int ErrorCode = 0;

            if (request.Document.fTransactionID == 0 && request.Document.fTransactionNo != null)            {
                
                String ErrorMessage = $"Either OrderId or OrderNo must be sent in request";
                response.ReturnInfo = new ReturnInfo() { ErrorCode = ErrorCode, ErrorMessage = ErrorMessage, ResultProcess = false };
                return response;
            }

            // Get Available Folio 
            var documentSrvc = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            var orderInfo = documentSrvc.GetOrderInfo(request.Document.fTransactionID, request.Document.fTransactionNo);
            var docEntity = AutoMapper.Mapper.Map<actblTaxDocument>(request.Document);

            if (orderInfo.MessageInfoResult != null && orderInfo.MessageInfoResult.ErrorCode  == 0)
            {
                docEntity.fAmount = orderInfo.OrderInfoResult.OrderAmount;
                docEntity.fDescription = $"Comision {orderInfo.OrderInfoResult.OrderNo}";// Description item
                //docEntity.fRecAgent = orderInfo.OrderInfoResult.reca
            }

            /*docEntity.fAmount = request.Document.TotalCharges.Value;
            docEntity.fCashRegisterNumber = request.Drawer;
            docEntity.fCashierName = request.CashierName;
            docEntity.fDescription = $"Comision {request.OrderNo}";// Description item
            docEntity.fRecAgent = request.RecAgent.Value;
            */

            docEntity.fAuthorizationNumber = GetFolioNumber(request.Document.fDocumentTypeID, request.Document.fRecAgentID);
            documentSrvc.CreateDocument(docEntity);
            documentSrvc.SaveChanges();

            return response;
        }

        internal string GetFolioNumber(int documentType, int recAgent = 0)
        {
            var folioNumber = 0;

            var caf = GetCaf(documentType, recAgent);
            if (caf != null)
            {
                folioNumber = int.Parse( caf.fCurrentNumber) + 1;

                if(folioNumber <= int.Parse( caf.fEndNumber))
                {
                    UpdateCafFolioCurrentNumber(caf.fAuthCodeID, folioNumber.ToString());
                }
            }          

            return folioNumber.ToString();
        }
        internal actblTaxDocument_AuthCode GetCaf(int documentType, int recAgent = 0)
        {
            var cafSrv = new CafService(new CafRepository(new ReceiptDbContext()));

            var caf = (from item in cafSrv.GetAllCafs()
                          where (documentType == item.fDocumentTypeID) &&
                                (recAgent == 0 || item.fRecAgentID == recAgent)
                          select item).FirstOrDefault();
            return caf;
        }
        internal void UpdateCafFolioCurrentNumber(int cafID, string folioNumber)
        {
            var cafSrv = new CafService(new CafRepository(new ReceiptDbContext()));
            var caf = (from item in cafSrv.GetAllCafs()
                          where item.fAuthCodeID == cafID
                          select item).FirstOrDefault();

            if (caf != null)
            {
                caf.fCurrentNumber = folioNumber;
            }

            cafSrv.UpdateCaf(caf);
            cafSrv.SaveChanges();
        }

        internal TaxSearchDocumentResponse SearchDocument(TaxSearchDocumentRequest r)
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

        internal TaxGenerateReceiptResponse GenerateReceipt(TaxGenerateReceiptRequest request)
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

        internal TaxSIIGetDocumentResponse SIIGetDocument(TaxSIIGetDocumentRequest request)
        {
            var _documentDownloader = new DocumentDownloader();
            var _documentHelper = new DocumentHandlerService(_documentService, _taxEntityService, _taxAddressService, _sequenceService, _storeService, 1);
            var response = new TaxSIIGetDocumentResponse();
            var docType = request.DocumentTypeID.ToString();
            var folio = request.Folio;
            var xmlContent = string.Empty;
            var _parserBoletas = new XmlDocumentParser<EnvioBOLETA>();
            var indexchunk = 1;
            var acumchunk = 0;
            var document = new actblTaxDocument();

            try
            {
                if (!ExistsFolioInDB(folio.ToString()) && _documentDownloader.RetrieveXML(int.Parse(docType), folio, out xmlContent))
                {
                    var documentXmlObject = _parserBoletas.GetDocumentObjectFromString(xmlContent);

                    List<int> detailids = null;
                    List<int> docids = null;
                    _documentHelper.SaveDocument(folio, folio, ref indexchunk, ref acumchunk, xmlContent, documentXmlObject, ref detailids, ref docids);
                }

                var dbDocument = GetDocumentByFolio(folio.ToString());
                var responseDocument = AutoMapper.Mapper.Map<TaxDocument>(dbDocument);
                response.Document = responseDocument;
                Logging.Log.Info("Returning Document");
                response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };

                return response;
            }
            catch (Exception ex)
            {
                Logging.Log.Error(ex.ToString());
                response.ReturnInfo = new ReturnInfo { ErrorCode = 2, ErrorMessage = ex.Message, ResultProcess = false };
            }
            finally
            {
                response.ResponseTime = DateTime.Now;
                response.TransferDate = DateTime.Now;
                response.ResponseTimeUTC = DateTime.UtcNow;
            }

            return response;        }
        internal TaxSIIGetDocumentBatchResponse CreateTaskSiiGetDocumentBatch(TaxSIIGetDocumentBatchRequest request)
        {
            var response = new TaxSIIGetDocumentBatchResponse();

            var taskService = new TaskService(new TaskRepository(new ReceiptDbContext()));

            var parameters = new DownloadBatchTaskParameter { FolioStart = request.FolioStart, FolioEnd = request.FolioEnd };
            var jsonParameters = JsonConvert.SerializeObject(parameters);

            var task = taskService.GetAllTasks().Where(m => m.fTypeID == 1 && m.fMethod == "Batch" && m.fRequestObject == jsonParameters).FirstOrDefault();

            if (task != null)
            {
                response.Status = $"Descargados {CountFoliosInDb(request.FolioStart.ToString(), request.FolioEnd.ToString()).ToString()} de {request.FolioEnd - request.FolioStart + 1}";
            }
            else
            {
                var id = taskService.GetAllTasks().Count() + 1;
                var newtask = new Domain.Core.Tasks.systblApp_CoreAPI_Task()
                {
                    fTaskID = id,
                    fTypeID = 1,
                    fStatus = 1,
                    fCountExecution = 0,
                    fStartDateTime = DateTime.Now,
                    fRequestObject = jsonParameters,
                    fMethod = "Batch"
                };

                taskService.CreateTask(newtask);

                taskService.SaveChanges();

                response.Status = $"Descarga iniciada de {request.FolioStart} a {request.FolioEnd}";

                var job = new BatchDownloadJob();
                BackgroundJob.Enqueue(() => job.Execute(id));
            }

            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };

            return response;
        }
        private actblTaxDocument GetDocumentByFolio(string folio)
        {
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceToSearch.GetAllDocuments().Where(m => m.fAuthorizationNumber == folio).FirstOrDefault();
        }
        private bool ExistsFolioInDB(string folio)
        {
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceToSearch.GetAllDocuments().Any(m => m.fAuthorizationNumber == folio);
        }

        private int CountFoliosInDb(string start, string end)
        {
            var folioStart = int.Parse(start);
            var folioEnd = int.Parse(end);
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceToSearch.GetAllDocuments().Count(m => int.Parse(m.fAuthorizationNumber) >= folioStart && int.Parse(m.fAuthorizationNumber) <= folioEnd);
        }
    }
}