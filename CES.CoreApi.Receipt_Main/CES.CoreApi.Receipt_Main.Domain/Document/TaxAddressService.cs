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
    public class TaxAddressService : ITaxAddressService
    {
        private ITaxAddressRepository repo;
        public TaxAddressService(ITaxAddressRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_CoreAPI_TaxAddress> GetAllTaxAddresss()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTaxAddress(systblApp_CoreAPI_TaxAddress objectEntry)
        {
            this.repo.CreateTaxAddress(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateTaxAddress(systblApp_CoreAPI_TaxAddress objectEntry)
        {
            this.repo.UpdateTaxAddress(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveTaxAddress(systblApp_CoreAPI_TaxAddress objectEntry)
        {
            this.repo.RemoveTaxAddress(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
