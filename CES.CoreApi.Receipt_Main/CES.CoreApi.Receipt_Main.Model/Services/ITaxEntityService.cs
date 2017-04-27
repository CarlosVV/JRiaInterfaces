using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaxEntityService
    {
        List<TaxEntity> GetAllTaxEntitys();
        void CreateTaxEntity(TaxEntity objectEntry);
        void UpdateTaxEntity(TaxEntity objectEntry);
        void RemoveTaxEntity(TaxEntity objectEntry);
        
    }
}
