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
        PersistenceModel GetPersistence(long persistenceID);
        PersistenceEventModel CreatePersistenceRequest<T>(IRequest objectToPerist, long persistenceID, int providerID, PersistenceEventType persistenceeventType);
        PersistenceEventModel CreatePersistenceRequest<T>(object request, long persistenceID, PersistenceEventType eventType);
        PersistenceEventModel CreatePersistence<T>(object objectToPerist, long persistenceID, int providerID, PersistenceEventType persistenceeventType);
        PersistenceEventModel CreatePersistence<T>(object objectToPerist, long persistenceID, PersistenceEventType persistenceeventType);
        PersistenceModel SetOrderToPersistence(long orderID, long persistenceID);        
    }
}
