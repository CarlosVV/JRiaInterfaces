﻿using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class DocumentDetailService : IDocumentDetailService
    {
        private IDocumentDetailRepository repo;
        public DocumentDetailService(IDocumentDetailRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_CoreAPI_DocumentDetail> GetAllDocumentDetails()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry)
        {
            this.repo.CreateDocumentDetail(objectEntry);
      
        }
        public void UpdateDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry)
        {
            this.repo.UpdateDocumentDetail(objectEntry);

        }
        public void RemoveDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry)
        {
            this.repo.RemoveDocumentDetail(objectEntry);

        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}