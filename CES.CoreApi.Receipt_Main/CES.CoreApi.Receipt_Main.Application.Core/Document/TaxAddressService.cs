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
        public List<actblTaxDocument_Entity_Address> GetAllTaxAddresss()
        {
            return repo.find(c => !c.fDisabled && !c.fDelete).ToList();
        }

        public void CreateTaxAddress(actblTaxDocument_Entity_Address objectEntry)
        {
            this.repo.CreateTaxAddress(objectEntry);      
        }
        public void UpdateTaxAddress(actblTaxDocument_Entity_Address objectEntry)
        {
            this.repo.UpdateTaxAddress(objectEntry);
        }
        public void RemoveTaxAddress(actblTaxDocument_Entity_Address objectEntry)
        {
            this.repo.RemoveTaxAddress(objectEntry);
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
