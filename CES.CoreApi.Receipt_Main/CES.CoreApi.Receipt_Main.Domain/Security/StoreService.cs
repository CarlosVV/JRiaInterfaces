using CES.CoreApi.Receipt_Main.Model;
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
            this.repo.SaveChanges();          
        }
        public void UpdateStore(systblApp_TaxReceipt_Store objectEntry)
        {
            this.repo.UpdateStore(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveStore(systblApp_TaxReceipt_Store objectEntry)
        {
            this.repo.RemoveStore(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
