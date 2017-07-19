using CES.CoreApi.Shared.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CES.CoreApi.Shared.Persistence.Model;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public class StandalonePersistence : IPersistenceHelper
    {
        public PersistenceEventModel CreatePersistence<T>(object objectToPerist, long persistenceID, PersistenceEventType persistenceeventType)
        {
            return null;
        }

        public PersistenceEventModel CreatePersistence<T>(object objectToPerist, long persistenceID, int providerID, PersistenceEventType persistenceeventType)
        {
            return null;
        }

        public PersistenceEventModel CreatePersistenceRequest<T>(object request, long persistenceID, PersistenceEventType eventType)
        {
            return null;
        }

        public PersistenceEventModel CreatePersistenceRequest<T>(IRequest objectToPerist, long persistenceID, int providerID, PersistenceEventType persistenceeventType)
        {
            return null;
        }

        public PersistenceModel GetPersistence(long persistenceID)
        {
            return null;
        }

        public PersistenceModel SetOrderToPersistence(long orderID, long persistenceID)
        {
            return null;
        }
    }
}