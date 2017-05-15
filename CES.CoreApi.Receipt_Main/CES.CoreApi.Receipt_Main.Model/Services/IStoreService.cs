using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IStoreService
    {
        List<systblApp_TaxReceipt_Store> GetAllStores();
        void CreateStore(systblApp_TaxReceipt_Store objectEntry);
        void UpdateStore(systblApp_TaxReceipt_Store objectEntry);
        void RemoveStore(systblApp_TaxReceipt_Store objectEntry);
        
    }
}
