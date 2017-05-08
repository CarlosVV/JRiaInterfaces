﻿using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class TaxEntityService : ITaxEntityService
    {
        private ITaxEntityRepository repo;
        public TaxEntityService(ITaxEntityRepository repository)
        {
            repo = repository;
        }
        public List<TaxEntity> GetAllTaxEntitys()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTaxEntity(TaxEntity objectEntry)
        {
            this.repo.CreateTaxEntity(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateTaxEntity(TaxEntity objectEntry)
        {
            this.repo.UpdateTaxEntity(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveTaxEntity(TaxEntity objectEntry)
        {
            this.repo.RemoveTaxEntity(objectEntry);
            this.repo.SaveChanges();
        }
    }
}