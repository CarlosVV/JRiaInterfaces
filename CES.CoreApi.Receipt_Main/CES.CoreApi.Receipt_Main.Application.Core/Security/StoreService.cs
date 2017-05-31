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
    public class StoreService : IStoreService
    {
        private IStoreRepository repo;
        public StoreService(IStoreRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_TaxReceipt_Store> GetAllStores()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateStore(systblApp_TaxReceipt_Store objectEntry)
        {
            this.repo.CreateStore(objectEntry);
          
        }
        public void UpdateStore(systblApp_TaxReceipt_Store objectEntry)
        {
            this.repo.UpdateStore(objectEntry);
           
        }
        public void RemoveStore(systblApp_TaxReceipt_Store objectEntry)
        {
            this.repo.RemoveStore(objectEntry);
            
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
