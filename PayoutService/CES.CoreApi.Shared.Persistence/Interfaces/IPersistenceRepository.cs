using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Interfaces
{
    public interface IPersistenceRepository
    {
        PersistenceModel GetPersistence(int persistenceID);
        PersistenceEventModel GetPersistenceEvent(int persistenceID, PersistenceEventType eventType);
        PersistenceModel CreatePersistence(PersistenceModel persistence);
        PersistenceModel UpdatePersistence(PersistenceModel persistence);
        PersistenceEventModel CreatePersistenceEvent(PersistenceEventModel persistenceEvent);
        RequesterInfoModel CreateRequesterInfo(RequesterInfoModel requesterInfo);
        


    }
}
