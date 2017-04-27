using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IFunctionalityService
    {
        List<Functionality> GetAllFunctionalitys();
        void CreateFunctionality(Functionality objectEntry);
        void UpdateFunctionality(Functionality objectEntry);
        void RemoveFunctionality(Functionality objectEntry);
        
    }
}
