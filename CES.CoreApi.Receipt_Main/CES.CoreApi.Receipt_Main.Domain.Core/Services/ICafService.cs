using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface ICafService
    {
        List<actblTaxDocument_AuthCode> GetAllCafs();
        void CreateCaf(actblTaxDocument_AuthCode objectEntry);
        void UpdateCaf(actblTaxDocument_AuthCode objectEntry);
        void RemoveCaf(actblTaxDocument_AuthCode objectEntry);
        void SaveChanges();
    }
}
