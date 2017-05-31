using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface ITaxEntityService
    {
        List<systblApp_CoreAPI_TaxEntity> GetAllTaxEntitys();
        void CreateTaxEntity(systblApp_CoreAPI_TaxEntity objectEntry);
        void UpdateTaxEntity(systblApp_CoreAPI_TaxEntity objectEntry);
        void RemoveTaxEntity(systblApp_CoreAPI_TaxEntity objectEntry);
        void SaveChanges();
    }
}
