using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ICafService
    {
        List<Caf> GetAllCafs();
        void CreateCaf(Caf objectEntry);
        void UpdateCaf(Caf objectEntry);
        void RemoveCaf(Caf objectEntry);
        
    }
}
