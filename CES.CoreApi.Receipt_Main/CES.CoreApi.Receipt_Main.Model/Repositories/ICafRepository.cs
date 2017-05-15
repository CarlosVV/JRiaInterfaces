using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ICafRepository
    {
        systblApp_CoreAPI_Caf find(string id);
        IEnumerable<systblApp_CoreAPI_Caf> find(Expression<Func<systblApp_CoreAPI_Caf, bool>> where);
        void CreateCaf(systblApp_CoreAPI_Caf obj);
        void UpdateCaf(systblApp_CoreAPI_Caf obj);
        void RemoveCaf(systblApp_CoreAPI_Caf obj);
        void SaveChanges();
    }
}
