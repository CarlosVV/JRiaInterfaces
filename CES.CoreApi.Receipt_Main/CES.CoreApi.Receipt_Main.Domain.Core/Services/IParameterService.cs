using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface IParameterService
    {
        List<systblApp_TaxReceipt_Parameter> GetAllParameters();
        void CreateParameter(systblApp_TaxReceipt_Parameter objectEntry);
        void UpdateParameter(systblApp_TaxReceipt_Parameter objectEntry);
        void RemoveParameter(systblApp_TaxReceipt_Parameter objectEntry);
        void SaveChanges();
    }
}
