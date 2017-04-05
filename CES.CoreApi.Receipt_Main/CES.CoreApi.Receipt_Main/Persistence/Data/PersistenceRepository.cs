using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlTypes;
using CES.Data.Sql;
using AutoMapper;
using System.Data.SqlClient;
using AutoMapper.Configuration;

namespace CES.CoreApi.Shared.Persistence.Data
{
    public class PersistenceRepository : IPersistenceRepository
    {
        #region Core
        private const string GetPersistenceByID = "coreApi_sp_PersistenceGetByID";
        private const string GetPersistenceEventData = "coreApi_sp_PersistenceEventGetByTypeID";
        private const string CreatePersistenceData = "coreApi_sp_PersistenceCreate";
        private const string CreatePresistenceEventData = "coreApi_sp_PersistenceEventCreate";
        private const string CreateRequesterInfoData = "coreApi_sp_RequestFromCreate";
        private const string UpdatePersistenceData = "coreApi_sp_PersistenceUpdate";
        private PersistenceSqlMapper SqlMapper = DatabaseName.CreatePersistenceSqlMapper();

        //private readonly IMapper _mapper;
        public PersistenceRepository()
        {
            //var cfg = new MapperConfigurationExpression();
            //cfg.AddProfile<PersistenceMapperProfile>();

            //var mapperConfig = new MapperConfiguration(cfg);
            //_mapper = new Mapper(mapperConfig);       
        }
        #endregion

        #region Public Methods
        public PersistenceModel GetPersistence(long persistenceID)
        {
            var PersistenceInfo = null as PersistenceModel;
            var PersistenceEventList = null as List<PersistenceEventModel>;
            var RequesterInfoList = null as List<RequesterInfoModel>;

            using (var sql = SqlMapper.CreateQueryAgain(DatabaseName.Main, GetPersistenceByID))
            {
                sql.AddParam("@fPersistenceID", persistenceID);
                //Log Call
                sql.LogSpCall();

                using (var reader = sql.DataQueryAgain.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        //PersistenceInfo = _mapper.Map<IDataReader, PersistenceModel>(reader);
                        PersistenceInfo = Mapper.Map<IDataReader, PersistenceModel>(reader);
                    }

                    if (PersistenceInfo == null) return null;

                    if(reader.NextResult())
                    {
                        PersistenceEventList = new List<PersistenceEventModel>();
                        while (reader.Read())
                        {
                            //PersistenceEventList.Add(_mapper.Map<IDataReader, PersistenceEventModel>(reader));
                            PersistenceEventList.Add(Mapper.Map<IDataReader, PersistenceEventModel>(reader));
                        }
                    }

                    if(reader.NextResult())
                    {
                        RequesterInfoList = new List<RequesterInfoModel>();
                        while (reader.Read())
                        {
                            //RequesterInfoList.Add(_mapper.Map<IDataReader, RequesterInfoModel>(reader));
                            RequesterInfoList.Add(Mapper.Map<IDataReader, RequesterInfoModel>(reader));
                        }
                    }
                }

                return PersistenceData(PersistenceInfo, PersistenceEventList, RequesterInfoList);
            }
        }

        public PersistenceModel CreatePersistence(PersistenceModel persistence)
        {
            using (var sql = SqlMapper.CreateCommand(DatabaseName.Main, CreatePersistenceData))
            {
                sql.AddParam("@fDisabled", persistence.Disabled);
                sql.AddParam("@fDelete", persistence.Delete);
                sql.AddParam("@fChanged", persistence.Changed);
                sql.AddParam("@fTime", persistence.Time);
                sql.AddParam("@fModified", persistence.Modified);
                sql.AddParam("@fModifiedID", persistence.ModifiedID);
                var fPersistenceID = sql.AddOutputParam("@fPersistenceID", SqlDbType.Int);
                //Log Call
                sql.LogSpCall();

                sql.Command.Execute(); 
                int PersistenceID = fPersistenceID.GetSafeValue<int>();
                persistence.PersistenceID = PersistenceID;
                return persistence;
            }
        }

        public PersistenceEventModel CreatePersistenceEvent(PersistenceEventModel persistenceEvent)
        {
            using (var sql = SqlMapper.CreateCommand(DatabaseName.Main, CreatePresistenceEventData))
            {
                sql.AddParam("@fPersistenceID", persistenceEvent.PersistenceID);
                sql.AddParam("@fAppID", persistenceEvent.AppID);
                sql.AddParam("@fProviderID", persistenceEvent.ProviderID);
                sql.AddParam("@fPersistenceEventTypeID", persistenceEvent.PersistenceEventTypeID);
                sql.AddParam("@fRequesterInfoID", persistenceEvent.RequesterInfo.RequesterInfoID);
                sql.AddParam("@fContent", persistenceEvent.GetPersistenceJson());
                sql.AddParam("@fDisabled", persistenceEvent.Disabled);
                sql.AddParam("@fDelete", persistenceEvent.Delete);
                sql.AddParam("@fChanged", persistenceEvent.Changed);
                sql.AddParam("@fTime", persistenceEvent.Time);
                sql.AddParam("@fModified", persistenceEvent.Modified);
                sql.AddParam("@fModifiedID", persistenceEvent.ModifiedID);
                var fPersistenceEventID = sql.AddOutputParam("@fPersistenceEventID", SqlDbType.Int);
                //Log Call
                sql.LogSpCall();

                sql.Command.Execute();
                int PersistenceEventID = fPersistenceEventID.GetSafeValue<int>();
                persistenceEvent.PersistenceEventID = PersistenceEventID;
                return persistenceEvent;
            }
        }

        public RequesterInfoModel CreateRequesterInfo(RequesterInfoModel requesterInfo)
        {
            DateTime minDate = (DateTime)SqlDateTime.MinValue;
            using (var sql = SqlMapper.CreateCommand(DatabaseName.Main, CreateRequesterInfoData))
            {
                sql.AddParam("@fAppID", requesterInfo.AppID);
                sql.AddParam("@fAppObjectID", requesterInfo.AppObjectID);
                sql.AddParam("@fAgentID", requesterInfo.AgentID);
                sql.AddParam("@fLocAgentID", requesterInfo.AgentLocID);
                sql.AddParam("@fUserID", requesterInfo.UserID);
                sql.AddParam("@fTerminalID", requesterInfo.TerminalID ?? "");
                sql.AddParam("@fTerminalUserID", requesterInfo.TerminalUserID ?? "");
                sql.AddParam("@fLocalTime", (requesterInfo.LocalTime == null || requesterInfo.LocalTime < minDate) ? minDate : requesterInfo.LocalTime);
                sql.AddParam("@fUtcTime", (requesterInfo.UtcTime == null || requesterInfo.UtcTime < minDate) ? minDate : requesterInfo.UtcTime);
                sql.AddParam("@fTimezone", requesterInfo.Timezone ?? "");
                sql.AddParam("@fLocale", requesterInfo.Locale ?? "");
                sql.AddParam("@fVersion", requesterInfo.Version ?? "");
                sql.AddParam("@fDisabled", requesterInfo.Disabled);
                sql.AddParam("@fDelete", requesterInfo.Delete);
                sql.AddParam("@fChanged", requesterInfo.Changed);
                sql.AddParam("@fTime", (requesterInfo.Time == null || requesterInfo.Time < minDate) ? minDate : requesterInfo.Time);
                sql.AddParam("@fModified", (requesterInfo.Modified == null || requesterInfo.Modified < minDate) ? minDate : requesterInfo.Modified);
                sql.AddParam("@fModifiedID", requesterInfo.ModifiedID);
                var fRequesterInfoID = sql.AddOutputParam("@fRequesterInfoID", SqlDbType.Int);
                //Log Call
                sql.LogSpCall();

                sql.Command.Execute();
                int RequesterInfoID = fRequesterInfoID.GetSafeValue<int>();
                requesterInfo.RequesterInfoID = RequesterInfoID;
                return requesterInfo;
            }
        }

        public void UpdatePersistence(PersistenceModel persistence)
        {
            var PersistenceInfo = null as PersistenceModel;
            var PersistenceEventList = null as List<PersistenceEventModel>;
            var RequesterInfoList = null as List<RequesterInfoModel>;
            using (var sql = SqlMapper.CreateCommand(DatabaseName.Main, UpdatePersistenceData))
            {
                sql.AddParam("@fPersistenceID", persistence.PersistenceID);
                sql.AddParam("@fOrderID", persistence.OrderID);
                sql.AddParam("@fDisabled", persistence.Disabled);
                sql.AddParam("@fDelete", persistence.Delete);
                sql.AddParam("@fChanged", persistence.Changed);
                sql.AddParam("@fTime", persistence.Time);
                sql.AddParam("@fModified", persistence.Modified);
                sql.AddParam("@fModifiedID", persistence.ModifiedID);
                //Log Call
                sql.LogSpCall();
                sql.Command.Execute();
            }
        }

        public PersistenceEventModel GetPersistenceEvent(int persistenceID, PersistenceEventType eventType)
        {
            var PersistenceEvent = null as PersistenceEventModel;
            var RequesterInfo = null as RequesterInfoModel;
            using (var sql = SqlMapper.CreateQueryAgain(DatabaseName.Main, GetPersistenceEventData))
            {
                sql.AddParam("@fPersistenceID", persistenceID);
                sql.AddParam("@fPersistenceEventTypeID", eventType);
                //Log Call
                sql.LogSpCall();
                using (var reader = sql.DataQueryAgain.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //PersistenceEvent = _mapper.Map<IDataReader, PersistenceEventModel>(reader);
                        PersistenceEvent = Mapper.Map<IDataReader, PersistenceEventModel>(reader);
                    }

                    if (reader.NextResult())
                    {
                        if (reader.Read())
                        {
                            //RequesterInfo = _mapper.Map<IDataReader, RequesterInfoModel>(reader);
                            RequesterInfo = Mapper.Map<IDataReader, RequesterInfoModel>(reader);
                        }
                    }
                }
            };
            PersistenceEvent.RequesterInfo = RequesterInfo;
            return PersistenceEvent;
        }
        #endregion

        #region Private Methods

        private PersistenceModel PersistenceData (PersistenceModel persistenceInfo, IEnumerable<PersistenceEventModel> persistenceEventList, IEnumerable<RequesterInfoModel> requesterInfoList)
        {

            persistenceInfo.PersistenceEvents = persistenceEventList.ToList();

            foreach (var persistenceEvent in persistenceInfo.PersistenceEvents)
            {
                var requesterInfo = requesterInfoList.FirstOrDefault(ri => ri.RequesterInfoID == persistenceEvent.RequesterInfoID);
                if (requesterInfo == null) continue;
                persistenceEvent.RequesterInfo = requesterInfo;
            }

            return persistenceInfo;
        }
        #endregion
    }
}