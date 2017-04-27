using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IStoreService
    {
        List<Store> GetAllStores();
        void CreateStore(Store objectEntry);
        void UpdateStore(Store objectEntry);
        void RemoveStore(Store objectEntry);
        
    }
}
