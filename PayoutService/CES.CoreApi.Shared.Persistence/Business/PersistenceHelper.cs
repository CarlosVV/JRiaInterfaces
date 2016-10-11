using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CES.CoreApi.Shared.Persistence.Business
{
    public class PersistenceHelper : IPersistenceHelper
    {
        #region Core
        private readonly IPersistenceRepository _repository;

        public PersistenceHelper(IPersistenceRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public methods
        public PersistenceModel GetPersistence(int persistenceID)
        {
            return GetPersistenceData(persistenceID);
        }

        public PersistenceEventModel CreatePersistenceEvent(PersistenceEventModel persistenceEvent)
        {
            return  CreatePersistenceEventData(persistenceEvent);    
        }
        #endregion

        #region Private methods
        private PersistenceModel GetPersistenceData(int persistenceID)
        {
            
            var reponses =  _repository.GetPersistence(persistenceID);

            if (reponses == null)
            {
                
            }

            return reponses;
        }

        private PersistenceEventModel CreatePersistenceEventData(PersistenceEventModel persistenceEvent)
        {
            using (var tx = new TransactionScope(TransactionScopeOption.Suppress))
            { 
                if (persistenceEvent.PersistenceID == 0)
                {
                    var persistence =  _repository.CreatePersistence(new PersistenceModel());
                    if (persistence == null) return persistenceEvent;
                    persistenceEvent.PersistenceID = persistence.PersistenceID;
                }

                var requesterInfoID = 0;
                if (IncludeRequesterInfo(persistenceEvent.PersistenceEventTypeID))
                {
                    var requesterInfo = _repository.CreateRequesterInfo(persistenceEvent.RequesterInfo);
                    if (requesterInfo == null) return persistenceEvent;
                    requesterInfoID = requesterInfo.RequesterInfoID;
                }

               
                persistenceEvent.RequesterInfoID = requesterInfoID;
                persistenceEvent = _repository.CreatePersistenceEvent(persistenceEvent);

                if (persistenceEvent == null) return persistenceEvent;

                tx.Complete();

            }
            return persistenceEvent;
            
        }

        public PersistenceModel SetOrderToPersistence(long orderID, int persistenceID)
        {
            var persistence = GetPersistenceData(persistenceID);
            persistence.OrderID = orderID;

            return _repository.UpdatePersistence(persistence);
        }

        private static bool IncludeRequesterInfo(PersistenceEventType persistenceEventType)
        {
            var requesterInfoIncluded = new List<PersistenceEventType>()
            {

                PersistenceEventType.PayoutPinRequestInfoRequest,
                PersistenceEventType.PayoutGetProviderInfoRequest,
                PersistenceEventType.PayoutRequest,
                PersistenceEventType.ComplianceCheckPayoutRequest,
                PersistenceEventType.ComplianceExternalCheckPayoutRequest,
                PersistenceEventType.PayoutTransactionRequest,
                PersistenceEventType.PayoutExternalConfirmationRequest                
            };

            return requesterInfoIncluded.Contains(persistenceEventType);
        }

      
        #endregion

    }
}

