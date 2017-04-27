using CES.CoreApi.Receipt_Main.Model.Documents;
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
        Store find(string id);
        IEnumerable<Store> find(Expression<Func<Store, bool>> where);
        void CreateStore(Store obj);
        void UpdateStore(Store obj);
        void RemoveStore(Store obj);
        void SaveChanges();
    }
}
