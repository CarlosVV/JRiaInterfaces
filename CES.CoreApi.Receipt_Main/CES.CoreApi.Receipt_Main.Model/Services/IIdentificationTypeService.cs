using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IIdentificationTypeService
    {
        List<IdentificationType> GetAllIdentificationTypes();
        void CreateIdentificationType(IdentificationType objectEntry);
        void UpdateIdentificationType(IdentificationType objectEntry);
        void RemoveIdentificationType(IdentificationType objectEntry);
        
    }
}
