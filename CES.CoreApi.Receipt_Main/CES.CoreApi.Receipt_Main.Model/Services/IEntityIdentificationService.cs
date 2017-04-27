using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IEntityIdentificationService
    {
        List<EntityIdentification> GetAllEntityIdentifications();
        void CreateEntityIdentification(EntityIdentification objectEntry);
        void UpdateEntityIdentification(EntityIdentification objectEntry);
        void RemoveEntityIdentification(EntityIdentification objectEntry);
        
    }
}
