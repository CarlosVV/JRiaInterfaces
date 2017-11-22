﻿using CES.CoreApi.Receipt_Main.Application.Core;
using CES.CoreApi.Receipt_Main.Application.Core.Document;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using CES.CoreApi.Receipt_Main.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Jobs
{
    public class BatchDownloadJob
    {
        private const int _maximum_quantity_of_document_errors = 50;
        private IDocumentService _documentService;
        private ITaxEntityService _taxEntityService;
        private ITaxAddressService _taxAddressService;
        private ISequenceService _sequenceService;
        private IStoreService _storeService;
        private ITaskService _taskService;
        private ITaskDetailService _taskDetailService;
        public void BatchDownload()
        {

        }
        public void Execute(int taskId)
        {
            _taskService = new TaskService(new TaskRepository(new ReceiptDbContext()));
            _taskDetailService = new TaskDetailService(new TaskDetailRepository(new ReceiptDbContext()));
            _documentService = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            _taxEntityService = new TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxAddressService = new TaxAddressService(new TaxAddressRepository(new ReceiptDbContext()));
            //_sequenceService = new SequenceService(new SequenceRepository(new ReceiptDbContext()));
            _sequenceService = new SequenceService();
            _storeService = new StoreService(new StoreRepository(new ReceiptDbContext()));

            var docType = "39";

            var task = _taskService.GetAllTasks().Where(m => m.fTaskID == taskId).FirstOrDefault();

            if (task == null) return;

            var parametersJson = task.fRequestObject;

            var parametersTask = JsonConvert.DeserializeObject<DownloadBatchTaskParameter>(parametersJson);
            var folioStart = parametersTask.FolioStart;
            var folioEnd = parametersTask.FolioEnd;
            var _documentHelper = new DocumentHandlerService(_documentService, _taxEntityService, _taxAddressService, _sequenceService, _storeService, 1);
            var _documentDownloader = new DocumentDownloader();
            var notFoundDocumentsFollowedQty = 0;

            UpdateTaskStatus(taskId);

            for (var folioIndex = folioStart; folioIndex <= folioEnd; folioIndex++)
            {
                var folio = folioIndex;
                var respuesta = string.Empty;
                var _parserBoletas = new XmlDocumentParser<EnvioBOLETA>();
                var indexchunk = 1;
                var acumchunk = 0;

                var taskDetailId = CreateTaskDetail(taskId, folio);

                if (ExistsFolioInDB(folio))
                {
                    UpdateTaskDetailForExistingFolio(taskDetailId, folio);

                    continue;
                }

                if (_documentDownloader.RetrieveXML(int.Parse(docType), folio, out respuesta))
                {
                    var documentXmlObject = _parserBoletas.GetDocumentObjectFromString(respuesta);

                    var documentId = CreateDocument(respuesta, _documentHelper, folio, ref indexchunk, ref acumchunk, documentXmlObject);

                    UpdateTaskDetailResultForDocumentCreation(taskDetailId, folio, documentId);
                }
                else
                {
                    notFoundDocumentsFollowedQty++;

                    if (notFoundDocumentsFollowedQty == _maximum_quantity_of_document_errors)
                    {
                        UpdateTaskDetailForNotFoundDocument(taskDetailId, folio);
                        break;
                    }
                }
            }

            UpdateTaskResult(taskId);
        }

        private int CreateDocument(string xmlDocument, DocumentHandlerService _documentHelper, int folio, ref int indexchunk, ref int acumchunk, EnvioBOLETA documentXmlObject)
        {
            List<int> detailids = null;
            List<int> docids = null;

            _documentHelper.SaveDocument(folio, folio, ref indexchunk, ref acumchunk, xmlDocument, documentXmlObject, ref detailids, ref docids);
            var documentId = GetDocumentId(folio);

            return documentId;
        }

        private void UpdateTaskStatus(int taskId)
        {
            var taskService = new TaskService(new TaskRepository(new ReceiptDbContext()));
            var task = taskService.GetAllTasks().Where(m => m.fTaskID == taskId).FirstOrDefault();

            if (task == null) return;

            task.fCountExecution = task.fCountExecution + 1;

            task.fThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            task.fStatus = 2;
            task.fLastExecutionDateTime = DateTime.Now;

            taskService.UpdateTask(task);
            taskService.SaveChanges();
        }

        private void UpdateTaskResult(int taskId)
        {
            var taskService = new TaskService(new TaskRepository(new ReceiptDbContext()));
            var task = taskService.GetAllTasks().Where(m => m.fTaskID == taskId).FirstOrDefault();

            if (task == null) return;

            task.fStatus = 3;
            task.fModified = DateTime.Now;
            task.fEndDateTime = DateTime.Now;
            taskService.UpdateTask(task);
            taskService.SaveChanges();
        }

        private void UpdateTaskDetailResultForDocumentCreation(long taskDetailId, int folio, int documentId)
        {
            var taskDetailService = new TaskDetailService(new TaskDetailRepository(new ReceiptDbContext()));
            var taskDetail = taskDetailService.GetAllTaskDetails().Where(m => m.fDetailID == taskDetailId).FirstOrDefault();

            if (taskDetail == null) return;

            taskDetail.fResultObject = $"Folio: {folio} descargado, DocumentId: {documentId} creado";
            taskDetail.fModified = DateTime.Now;
            taskDetail.fDocumentID = documentId;
            taskDetailService.UpdateTaskDetail(taskDetail);
            taskDetailService.SaveChanges();
        }

        private void UpdateTaskDetailForExistingFolio(long taskDetailId, int folio)
        {
            var taskDetailService = new TaskDetailService(new TaskDetailRepository(new ReceiptDbContext()));
            var taskDetail = taskDetailService.GetAllTaskDetails().Where(m => m.fDetailID == taskDetailId).FirstOrDefault();

            if (taskDetail == null) return;

            taskDetail.fDocumentID = 0;
            taskDetail.fModified = DateTime.Now;
            taskDetail.fResultObject = $"Folio {folio} existe en BD";
            taskDetailService.UpdateTaskDetail(taskDetail);
            taskDetailService.SaveChanges();
        }

        private void UpdateTaskDetailForNotFoundDocument(long taskDetailId, int folio)
        {
            var taskDetailService = new TaskDetailService(new TaskDetailRepository(new ReceiptDbContext()));
            var taskDetail = taskDetailService.GetAllTaskDetails().Where(m => m.fDetailID == taskDetailId).FirstOrDefault();

            if (taskDetail == null) return;

            taskDetail.fDocumentID = 0;
            taskDetail.fModified = DateTime.Now;
            taskDetail.fResultObject = $"Folio {folio} cannot be downloaded. Maximum quantity of attempts reached: {_maximum_quantity_of_document_errors}";
            taskDetailService.UpdateTaskDetail(taskDetail);
            taskDetailService.SaveChanges();
        }

        private long CreateTaskDetail(long taskId, int folio)
        {
            var taskDetailService = new TaskDetailService(new TaskDetailRepository(new ReceiptDbContext()));
            var taskIdDetailId = taskDetailService.GetAllTaskDetails().Count() + 1;
            var taskDetail = new Domain.Core.Tasks.systblApp_CoreAPI_Task_Detail
            {
                fDetailID = taskIdDetailId,
                fTaskID = taskId,
                fStateObject = $"Folio: {folio}",

            };

            taskDetailService.CreateTaskDetail(taskDetail);
            taskDetailService.SaveChanges();
            return taskDetail.fDetailID;
        }

        private bool ExistsFolioInDB(int folio)
        {
            var folioNumber = folio.ToString();
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceToSearch.GetAllDocuments().Any(m => m.fAuthorizationNumber == folioNumber);
        }

        private int GetDocumentId(int folio)
        {
            var folioNumber = folio.ToString();
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            var obj = _documentServiceToSearch.GetAllDocuments().FirstOrDefault(m => m.fAuthorizationNumber == folioNumber);
            return obj != null ? obj.fDocumentID : 0;
        }
    }
}