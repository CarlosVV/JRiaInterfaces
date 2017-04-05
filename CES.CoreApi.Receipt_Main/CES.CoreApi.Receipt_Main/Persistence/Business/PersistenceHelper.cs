using AutoMapper;
using AutoMapper.Configuration;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using System.Transactions;

namespace CES.CoreApi.Shared.Persistence.Business
{
    public class PersistenceHelper : IPersistenceHelper
    {
        #region Core
        private readonly IPersistenceRepository _repository;
        //private readonly IMapper _mapper;
        public PersistenceHelper(IPersistenceRepository repository)
        {
            _repository = repository;

            //var cfg = new MapperConfigurationExpression();
            //cfg.CreateMap<IRequesterInfo, RequesterInfoModel>();

            //var mapperConfig = new MapperConfiguration(cfg);
            //_mapper = new Mapper(mapperConfig);
            Mapper.CreateMap<IRequesterInfo, RequesterInfoModel>();       
           
        }

        #endregion

        #region Public methods
        public PersistenceModel GetPersistence(long persistenceID)
        {
            return GetPersistenceData(persistenceID);
        }
              

        public  PersistenceEventModel CreatePersistenceRequest<T>(IRequest objectToPersist, long persistenceID,int providerID, PersistenceEventType persistenceeventType)
        {
            var persistenceEvent = new PersistenceEventModel(persistenceID, providerID, persistenceeventType);
            persistenceEvent.SetContentObject<T>(objectToPersist);
        

            if (objectToPersist is IRequest && objectToPersist.RequesterInfo != null)
            {                
                //persistenceEvent.RequesterInfo  = _mapper.Map<RequesterInfoModel>(objectToPersist.RequesterInfo);
                persistenceEvent.RequesterInfo = Mapper.Map<RequesterInfoModel>(objectToPersist.RequesterInfo);
                persistenceEvent.SaveRequestInfo = true;        
            }
            return CreatePersistenceEvent(persistenceEvent);
        }
        public PersistenceEventModel CreatePersistenceRequest<T>(object objectToPersist, long persistenceID, PersistenceEventType persistenceeventType)
        {
            var persistenceEvent = new PersistenceEventModel(persistenceID, 0, persistenceeventType);
            persistenceEvent.SetContentObject<T>(objectToPersist);
            return CreatePersistenceEvent(persistenceEvent);
        }

        public PersistenceEventModel CreatePersistence<T>(object objectToPersist, long persistenceID, int providerID, PersistenceEventType persistenceeventType)
        {
            var persistenceEvent = new PersistenceEventModel(persistenceID, providerID, persistenceeventType);
            persistenceEvent.SetContentObject<T>(objectToPersist);
           
            return CreatePersistenceEvent(persistenceEvent);
        }

        public PersistenceEventModel CreatePersistence<T>(object objectToPersist, long persistenceID, PersistenceEventType persistenceeventType)
        {
            return this.CreatePersistence<T>(objectToPersist, persistenceID, 0, persistenceeventType);
        }

        public PersistenceModel SetOrderToPersistence(long orderID, long persistenceID)
        {
            var persistence = GetPersistenceData(persistenceID);
            persistence.OrderID = orderID;

             _repository.UpdatePersistence(persistence);

            return persistence;
        }

        #endregion

        #region Private methods

        private PersistenceEventModel CreatePersistenceEvent(PersistenceEventModel persistenceEvent)
        {
            return CreatePersistenceEventData(persistenceEvent);
        }

        private PersistenceModel GetPersistenceData(long persistenceID)
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
                //if (IncludeRequesterInfo(persistenceEvent.PersistenceEventTypeID))
                if(persistenceEvent.SaveRequestInfo)
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
        
        #endregion
    }
}

