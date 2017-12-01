using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface ITaxAddressService
    {
        List<actblTaxDocument_Entity_Address> GetAllTaxAddresses();
        void CreateTaxAddress(actblTaxDocument_Entity_Address objectEntry);
        void UpdateTaxAddress(actblTaxDocument_Entity_Address objectEntry);
        void RemoveTaxAddress(actblTaxDocument_Entity_Address objectEntry);
        void SaveChanges();
    }
}
