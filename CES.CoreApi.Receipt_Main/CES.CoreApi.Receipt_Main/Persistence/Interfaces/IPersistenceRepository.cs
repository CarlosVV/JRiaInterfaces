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
        PersistenceModel GetPersistence(long persistenceID);
        PersistenceModel CreatePersistence(PersistenceModel persistence);
        PersistenceEventModel CreatePersistenceEvent(PersistenceEventModel persistenceEvent);
        RequesterInfoModel CreateRequesterInfo(RequesterInfoModel requesterInfo);
        void UpdatePersistence(PersistenceModel persistence);
        PersistenceEventModel GetPersistenceEvent(int persistenceID, PersistenceEventType eventType);
    }
}
