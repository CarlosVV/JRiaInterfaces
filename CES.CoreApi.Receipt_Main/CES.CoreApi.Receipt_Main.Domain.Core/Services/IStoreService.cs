using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface IStoreService
    {
        List<systblApp_TaxReceipt_Store> GetAllStores();
        void CreateStore(systblApp_TaxReceipt_Store objectEntry);
        void UpdateStore(systblApp_TaxReceipt_Store objectEntry);
        void RemoveStore(systblApp_TaxReceipt_Store objectEntry);
        void SaveChanges();
    }
}
