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
            _documentService = new CES.CoreApi.Receipt_Main.Application.Core.DocumentService(new DocumentRepository(new ReceiptDbContext()));
            _taxEntityService = new CES.CoreApi.Receipt_Main.Application.Core.TaxEntityService(new TaxEntityRepository(new ReceiptDbContext()));
            _taxAddressService = new CES.CoreApi.Receipt_Main.Application.Core.TaxAddressService(new TaxAddressRepository(new ReceiptDbContext()));
            _sequenceService = new CES.CoreApi.Receipt_Main.Application.Core.SequenceService(new SequenceRepository(new ReceiptDbContext()));
            _storeService = new CES.CoreApi.Receipt_Main.Application.Core.StoreService(new StoreRepository(new ReceiptDbContext()));

            var _documentDownloader = new DocumentDownloader();
            var _documentHelper = new DocumentHandlerService(_documentService, _taxEntityService, _taxAddressService, _sequenceService, _storeService);

            var docType = "39";

            var task = _taskService.GetAllTasks().Where(m => m.Id == taskId).FirstOrDefault();

            if (task == null) return;

            var parametersJson = task.RequestObject;

            var parametersTask = JsonConvert.DeserializeObject<DownloadBatchTaskParameter>(parametersJson);
            var folioStart = parametersTask.FolioStart;
            var folioEnd = parametersTask.FolioEnd;

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
                    UpdateTaskDetail(taskDetailId, folio);

                    continue;
                }

                if (_documentDownloader.RetrieveXML(int.Parse(docType), folio, out respuesta))
                {
                    var documentXmlObject = _parserBoletas.GetDocumentObjectFromString(respuesta);

                    List<int> detailids = null;
                    List<int> docids = null;

                    _documentHelper.SaveDocument(folio, folio, ref indexchunk, ref acumchunk, documentXmlObject, ref detailids, ref docids);

                    UpdateTaskDetailResult(taskDetailId, folio);

                }
            }

            UpdateTaskResult(taskId);
        }

        private void UpdateTaskStatus(int taskId)
        {
            var taskService = new TaskService(new TaskRepository(new ReceiptDbContext()));
            var task = taskService.GetAllTasks().Where(m => m.Id == taskId).FirstOrDefault();
            task.CountExecution = task.CountExecution + 1;

            task.ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            task.Status = 2;
            task.LastExecutionDateTime = DateTime.Now;

            taskService.UpdateTask(task);
            taskService.SaveChanges();
        }

        private void UpdateTaskResult(int taskId)
        {
            var taskService = new TaskService(new TaskRepository(new ReceiptDbContext()));
            var task = taskService.GetAllTasks().Where(m => m.Id == taskId).FirstOrDefault();
            task.Status = 3;
            task.fModified = DateTime.Now;
            task.EndDateTime = DateTime.Now;
            taskService.UpdateTask(task);
            taskService.SaveChanges();
        }

        private void UpdateTaskDetailResult(int taskDetailId, int folio)
        {
            var taskDetailService = new TaskDetailService(new TaskDetailRepository(new ReceiptDbContext()));
            var taskDetail = taskDetailService.GetAllTaskDetails().Where(m => m.Id == taskDetailId).FirstOrDefault();

            taskDetail.ResultObject = $"Folio {folio} descargado";
            taskDetail.fModified = DateTime.Now;
            taskDetail.DocumentId = folio;
            taskDetailService.UpdateTaskDetail(taskDetail);
            taskDetailService.SaveChanges();
        }

        private void UpdateTaskDetail(int taskDetailId, int folio)
        {
            var taskDetailService = new TaskDetailService(new TaskDetailRepository(new ReceiptDbContext()));
            var taskDetail = taskDetailService.GetAllTaskDetails().Where(m => m.Id == taskDetailId).FirstOrDefault();

            taskDetail.fModified = DateTime.Now;
            taskDetail.ResultObject = $"Folio {folio} existe en BD";
            taskDetailService.UpdateTaskDetail(taskDetail);
            taskDetailService.SaveChanges();
        }

        private int CreateTaskDetail(int taskId, int folio)
        {
            var taskDetailService = new TaskDetailService(new TaskDetailRepository(new ReceiptDbContext()));
            var taskIdDetailId = taskDetailService.GetAllTaskDetails().Count() + 1;
            var taskDetail = new Domain.Core.Tasks.systblApp_CoreAPI_TaskDetail
            {
                Id = taskIdDetailId,
                TaskId = taskId,
                StateObject = $"Folio: {folio}",

            };

            taskDetailService.CreateTaskDetail(taskDetail);
            taskDetailService.SaveChanges();
            return taskDetail.Id;
        }

        private bool ExistsFolioInDB(int folio)
        {
            var _documentServiceToSearch = new DocumentService(new DocumentRepository(new ReceiptDbContext()));
            return _documentServiceToSearch.GetAllDocuments().Any(m => m.Folio == folio);
        }
    }
}