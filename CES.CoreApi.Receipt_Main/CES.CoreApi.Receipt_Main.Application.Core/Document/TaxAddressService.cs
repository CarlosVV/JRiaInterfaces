using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
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
        }
        public void UpdateTaxAddress(systblApp_CoreAPI_TaxAddress objectEntry)
        {
            this.repo.UpdateTaxAddress(objectEntry);
        }
        public void RemoveTaxAddress(systblApp_CoreAPI_TaxAddress objectEntry)
        {
            this.repo.RemoveTaxAddress(objectEntry);
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
