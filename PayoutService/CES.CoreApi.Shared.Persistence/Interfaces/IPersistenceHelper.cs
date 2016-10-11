using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Interfaces
{
    public interface IPersistenceHelper
    {
        PersistenceModel GetPersistence(int persistenceID);
        PersistenceEventModel CreatePersistenceEvent(PersistenceEventModel persistenceEvent);
        PersistenceModel SetOrderToPersistence(long orderID, int persistenceID);
       
    }
}
