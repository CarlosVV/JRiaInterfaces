using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IStoreRepository
    {
        systblApp_TaxReceipt_Store find(string id);
        IEnumerable<systblApp_TaxReceipt_Store> find(Expression<Func<systblApp_TaxReceipt_Store, bool>> where);
        void CreateStore(systblApp_TaxReceipt_Store obj);
        void UpdateStore(systblApp_TaxReceipt_Store obj);
        void RemoveStore(systblApp_TaxReceipt_Store obj);
        void SaveChanges();
    }
}
