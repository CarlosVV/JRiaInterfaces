using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
//using CES.CoreApi.Receipt_Main.Models.DTOs;

namespace CES.CoreApi.Receipt_Main.Services
{
    public class DocumentService
    {
        private DocumentRepository _documentrepository;
        private DocumentDetailRepository _detallerepository;
        private ItemRepository _itemrepository;
        private SubjectRepository _subjectrepository;
        public DocumentService()
        {
            _documentrepository = new DocumentRepository();
            _detallerepository = new DocumentDetailRepository();
            _itemrepository = new ItemRepository();
            _subjectrepository = new SubjectRepository();
        }


        internal TaxCreateDocumentResponse CreateDocument(TaxCreateDocumentRequest request)
        {
            var response = new TaxCreateDocumentResponse();

            using (var tx = new TransactionScope(TransactionScopeOption.Suppress))
            {
                if(request.Document.SenderId == null && request.Document.Sender != null)
                {
                    //_subjectrepository.CreateTaxEntity(request.Document.Sender);
                    request.Document.SenderId = request.Document.Sender.Id;
                }

                if (request.Document.ReceiverId == null && request.Document.Receiver != null)
                {
                    //_subjectrepository.CreateTaxEntity(request.Document.Receiver);
                    request.Document.ReceiverId = request.Document.Receiver.Id;
                }

                //_documentrepository.Create(request.Document);

                foreach (var dtl in request.Document.DocumentDetails)
                {
                    dtl.DocumentId = request.Document.Id;
                    if (dtl.ItemId != null && dtl.Item != null)
                    {
                        //_itemrepository.Create(dtl.Item);
                        dtl.ItemId = dtl.Item.Id;
                    }
                   
                    //_detallerepository.Create(dtl);
                }

                tx.Complete();
            }

            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo {ErrorCode  = 1, ErrorMessage = "Process Done", ResultProcess = true};
            return response;
        }

        internal object SearchDocument(TaxSearchDocumentRequest r)
        {
            var response = new TaxSearchDocumentResponse();

            var results = _documentrepository.Search(r.DocumentId, r.DocumentTypeCode, r.ItemCode, r.ReceiverFirstName, r.ReceiverMiddleName, r.ReceiverLastName1, r.ReceiverLastName2, r.SenderFirstName,
                r.SenderMiddleName, r.SenderLastName1, r.SenderLastName2, r.DocumentFolio, r.DocumentBranch, r.DocumentTellerNumber, r.DocumentTellerName, r.DocumentIssued, r.DocumentTotalAmount);

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

            var document = null as List<Document>;// = _documentrepository.GetByOrderNumberAndFolio(request.OrderNumber, request.Folio);

            response.Document = document.FirstOrDefault();
            
            //TODO: Populate all depending objects in the model
            PopulateDocumentModel(response.Document);

            //TODO: Create PDF for Document Stored
            CreatePDF(response.Document);

            
            response.ResponseTime = DateTime.Now;
            response.TransferDate = DateTime.Now;
            response.ResponseTimeUTC = DateTime.UtcNow;
            response.ReturnInfo = new ReturnInfo { ErrorCode = 1, ErrorMessage = "Process Done", ResultProcess = true };
            return response;
        }

        private void CreatePDF(Document document)
        {
            throw new NotImplementedException();
        }

        private void PopulateDocumentModel(Document document)
        {
            throw new NotImplementedException();
        }

        internal TaxSIIGetDocumentBatchResponse CreateTaskSiiGetDocumentBatch(TaxSIIGetDocumentBatchRequest taxSIIGetDocumentBatchInternalRequest)
        {
            var response = null as TaxSIIGetDocumentBatchResponse;



            return response;
        }
    }
}