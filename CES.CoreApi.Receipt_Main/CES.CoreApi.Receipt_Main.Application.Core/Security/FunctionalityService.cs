using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
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
         
        }
        public void UpdateFunctionality(systblApp_TaxReceipt_Functionality objectEntry)
        {
            this.repo.UpdateFunctionality(objectEntry);
          
        }
        public void RemoveFunctionality(systblApp_TaxReceipt_Functionality objectEntry)
        {
            this.repo.RemoveFunctionality(objectEntry);
        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
