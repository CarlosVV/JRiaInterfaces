﻿using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Security;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class FunctionalityService : IFunctionalityService
    {
        private IFunctionalityRepository repo;
        public FunctionalityService(IFunctionalityRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_TaxReceipt_Functionality> GetAllFunctionalitys()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateFunctionality(systblApp_TaxReceipt_Functionality objectEntry)
        {
            this.repo.CreateFunctionality(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateFunctionality(systblApp_TaxReceipt_Functionality objectEntry)
        {
            this.repo.UpdateFunctionality(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveFunctionality(systblApp_TaxReceipt_Functionality objectEntry)
        {
            this.repo.RemoveFunctionality(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
