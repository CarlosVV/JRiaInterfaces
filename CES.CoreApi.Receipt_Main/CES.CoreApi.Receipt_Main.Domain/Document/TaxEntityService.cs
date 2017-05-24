using CES.CoreApi.Receipt_Main.Model.Documents;
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
        public List<systblApp_CoreAPI_TaxEntity> GetAllTaxEntitys()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTaxEntity(systblApp_CoreAPI_TaxEntity objectEntry)
        {
            this.repo.CreateTaxEntity(objectEntry);      
        }
        public void UpdateTaxEntity(systblApp_CoreAPI_TaxEntity objectEntry)
        {
            this.repo.UpdateTaxEntity(objectEntry);
        }
        public void RemoveTaxEntity(systblApp_CoreAPI_TaxEntity objectEntry)
        {
            this.repo.RemoveTaxEntity(objectEntry);
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
