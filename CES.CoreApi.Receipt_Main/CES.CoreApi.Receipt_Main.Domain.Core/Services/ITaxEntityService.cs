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
        List<actblTaxDocument_Entity> GetAllTaxEntitys();
        void CreateTaxEntity(actblTaxDocument_Entity objectEntry);
        void UpdateTaxEntity(actblTaxDocument_Entity objectEntry);
        void RemoveTaxEntity(actblTaxDocument_Entity objectEntry);
        void SaveChanges();
    }
}
