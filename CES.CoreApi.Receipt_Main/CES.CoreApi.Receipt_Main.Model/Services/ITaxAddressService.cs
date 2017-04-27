using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaxAddressService
    {
        List<TaxAddress> GetAllTaxAddresss();
        void CreateTaxAddress(TaxAddress objectEntry);
        void UpdateTaxAddress(TaxAddress objectEntry);
        void RemoveTaxAddress(TaxAddress objectEntry);
        
    }
}
