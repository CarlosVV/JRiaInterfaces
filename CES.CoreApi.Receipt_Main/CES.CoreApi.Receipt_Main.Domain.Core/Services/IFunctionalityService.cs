using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface IFunctionalityService
    {
        List<systblApp_TaxReceipt_Functionality> GetAllFunctionalitys();
        void CreateFunctionality(systblApp_TaxReceipt_Functionality objectEntry);
        void UpdateFunctionality(systblApp_TaxReceipt_Functionality objectEntry);
        void RemoveFunctionality(systblApp_TaxReceipt_Functionality objectEntry);
        void SaveChanges();
    }
}
