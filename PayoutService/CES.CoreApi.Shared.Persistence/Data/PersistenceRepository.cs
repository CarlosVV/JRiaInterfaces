
using CES.CoreApi.Data.Base;
using CES.CoreApi.Data.Models;
using CES.CoreApi.Data.Enumerations;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;

using CES.CoreApi.Shared.Persistence.Constants;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace CES.CoreApi.Shared.Persistence.Data
{
    public class PersistenceRepository : BaseGenericRepository, IPersistenceRepository
    {
        #region Core
        private static readonly Log4NetProxy _logger = new Log4NetProxy();
    
        #endregion

        #region Public Methods
        public PersistenceModel GetPersistence(int persistenceID)
        {
            var request = new DatabaseRequest<PersistenceModel>
            {
                ProcedureName = StoreProcedureConstants.GetPersistenceData,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fPersistenceID",persistenceID)
                   
                },
                Shaper = reader => GetPersistenceData(reader)
            };
            return  Get(request);
        }
        public PersistenceEventModel GetPersistenceEvent(int persistenceID, PersistenceEventType eventType)
        {
            var request = new DatabaseRequest<PersistenceEventModel>
            {
                ProcedureName = StoreProcedureConstants.GetPersistenceData,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fPersistenceID",persistenceID),
                    new SqlParameter("@fPersistenceEventTypeID",eventType)

                },
                Shaper = reader => GetPersistenceEventData(reader)
            };
            return Get(request);
        }
        public PersistenceModel CreatePersistence(PersistenceModel persistence)
        {
            var request = new DatabaseRequest<PersistenceModel>
            {
                ProcedureName = StoreProcedureConstants.CreatePersistenceData,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fDisabled", persistence.Disabled) ,
                    new SqlParameter("@fDelete", persistence.Delete),
                    new SqlParameter("@fChanged", persistence.Changed) ,
                    new SqlParameter("@fTime", persistence.Time),
                    new SqlParameter("@fModified", persistence.Modified) ,
                    new SqlParameter("@fModifiedID", persistence.ModifiedID) ,
                }
                .AddIntOut("@fPersistenceID"),
                OutputFuncShaper = parameters => SetPersistenceID(parameters, persistence)
            };

            try
            {
                return  Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (Create Persistence) " + e.Message);
                _logger.PublishInformation("Error Calling Database: (Create Persistence) " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (Create Persistence) " + e.Message);
            }
        }

        public PersistenceModel UpdatePersistence(PersistenceModel persistence)
        {
            var request = new DatabaseRequest<PersistenceModel>
            {
                ProcedureName = StoreProcedureConstants.UpdatePersistenceData,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fPersistenceID", persistence.PersistenceID) ,
                    new SqlParameter("@fOrderID", persistence.OrderID) ,
                    new SqlParameter("@fDisabled", persistence.Disabled) ,
                    new SqlParameter("@fDelete", persistence.Delete),
                    new SqlParameter("@fChanged", persistence.Changed) ,
                    new SqlParameter("@fTime", persistence.Time),
                    new SqlParameter("@fModified", persistence.Modified) ,
                    new SqlParameter("@fModifiedID", persistence.ModifiedID) ,
                },
                Shaper = reader => GetPersistenceData(reader)
            };

            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (Create Persistence) " + e.Message);
                _logger.PublishInformation("Error Calling Database: (Create Persistence) " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (Create Persistence) " + e.Message);
            }
        }
        public PersistenceEventModel CreatePersistenceEvent(PersistenceEventModel persistenceEvent)
        {
            var request = new DatabaseRequest<PersistenceEventModel>
            {
                ProcedureName = StoreProcedureConstants.CreatePresistenceEventData,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fPersistenceID", persistenceEvent.PersistenceID) ,
                    new SqlParameter("@fAppID", persistenceEvent.AppID),
                    new SqlParameter("@fProviderID", persistenceEvent.ProviderID) ,
                    new SqlParameter("@fPersistenceEventTypeID", persistenceEvent.PersistenceEventTypeID),
                    new SqlParameter("@fRequesterInfoID", persistenceEvent.RequesterInfo.RequesterInfoID) ,
                    new SqlParameter("@fContent", persistenceEvent.GetPersistenceJson()),
                    new SqlParameter("@fDisabled", persistenceEvent.Disabled) ,
                    new SqlParameter("@fDelete", persistenceEvent.Delete),
                    new SqlParameter("@fChanged", persistenceEvent.Changed) ,
                    new SqlParameter("@fTime", persistenceEvent.Time),
                    new SqlParameter("@fModified", persistenceEvent.Modified) ,
                    new SqlParameter("@fModifiedID", persistenceEvent.ModifiedID) ,
                }
                .AddIntOut("@fPersistenceEventID"),
                OutputFuncShaper = parameters => SetPersistenceEventID(parameters, persistenceEvent)              
            };

            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (CreatePersistenceEvent) " + e.Message);
                _logger.PublishInformation("Error Calling Database: (CreatePersistenceEvent) " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (CreatePersistenceEvent) " + e.Message);
            }
        }
        public RequesterInfoModel CreateRequesterInfo(RequesterInfoModel requesterInfo)
        {
            DateTime minDate = (DateTime)SqlDateTime.MinValue;

            var request = new DatabaseRequest<RequesterInfoModel>
            {
                ProcedureName = StoreProcedureConstants.CreateRequesterInfoData,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fAppID", requesterInfo.AppID) ,
                    new SqlParameter("@fAppObjectID", requesterInfo.AppObjectID) ,
                    new SqlParameter("@fAgentID", requesterInfo.AgentID),
                    new SqlParameter("@fLocAgentID", requesterInfo.LocAgentID) ,
                    new SqlParameter("@fUserID", requesterInfo.UserID),
                    new SqlParameter("@fTerminalID", requesterInfo.TerminalID ?? "") ,
                    new SqlParameter("@fTerminalUserID", requesterInfo.TerminalUserID ?? ""),
                    new SqlParameter("@fLocalTime", (requesterInfo.LocalTime == null || requesterInfo.LocalTime < minDate) ? minDate : requesterInfo.LocalTime),
                    new SqlParameter("@fUtcTime", (requesterInfo.UtcTime == null || requesterInfo.UtcTime < minDate) ? minDate : requesterInfo.UtcTime),
                    new SqlParameter("@fTimezone", requesterInfo.TimeZone ?? "") ,
                    new SqlParameter("@fLocale", requesterInfo.Locale ?? "") ,
                    new SqlParameter("@fVersion", requesterInfo.Version ?? "") ,
                    new SqlParameter("@fDisabled", requesterInfo.Disabled) ,
                    new SqlParameter("@fDelete", requesterInfo.Delete) ,
                    new SqlParameter("@fChanged", requesterInfo.Changed),
                    new SqlParameter("@fTime", (requesterInfo.Time == null || requesterInfo.Time < minDate) ? minDate : requesterInfo.Time),
                    new SqlParameter("@fModified", (requesterInfo.Modified == null || requesterInfo.Modified < minDate) ? minDate : requesterInfo.Modified) ,
                    new SqlParameter("@fModifiedID", requesterInfo.ModifiedID) ,
                }
                .AddIntOut("@fRequesterInfoID"),
                OutputFuncShaper = parameters => SetRequesterInfoModelID(parameters, requesterInfo)
            };

            try
            {
                return  Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (CreatePersistenceRequesterInfo) " + e.Message);
                _logger.PublishInformation("Error Calling Database: (CreatePersistenceRequesterInfo) " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (CreatePersistenceRequesterInfo) " + e.Message);
            }
        }

        #endregion

        #region Private Methods
        private static PersistenceModel GetPersistenceData(IDataReader reader)
        {
            var persistence = new PersistenceModel();
           
            persistence.PersistenceID = reader.ReadValue<int>("fPersistenceID");
            persistence.Disabled = reader.ReadValue<bool>("fDisabled");
            persistence.Delete = reader.ReadValue<bool>("fDelete");
            persistence.Changed = reader.ReadValue<bool>("fChanged");
            persistence.Time = reader.ReadValue<DateTime>("fTime");
            persistence.Modified = reader.ReadValue<DateTime>("fModified");
            persistence.ModifiedID = reader.ReadValue<int>("fModifiedID");

            reader.NextResult();
            persistence.PersistenceEvents = GetPersistenceEventsData(reader);         

            reader.NextResult();
            var requesterInfoList = GetRequesterInfoListData(reader);

            foreach ( var persistenceEvent in persistence.PersistenceEvents )
            {
                var requesterInfo = requesterInfoList.FirstOrDefault(ri => ri.RequesterInfoID == persistenceEvent.RequesterInfoID);
                if (requesterInfo == null) continue;
                persistenceEvent.RequesterInfo = requesterInfo;
            }
            return persistence;
        }
        private static PersistenceEventModel GetPersistenceEventData(IDataReader reader)
        {
            PersistenceEventModel persistenceEvent = null;         
            persistenceEvent = new PersistenceEventModel();
            persistenceEvent.PersistenceEventID = reader.ReadValue<int>("fPersistenceEventID");
            persistenceEvent.PersistenceID = reader.ReadValue<int>("fPersistenceID");
            persistenceEvent.AppID = reader.ReadValue<int>("fAppID");
            persistenceEvent.ProviderID = reader.ReadValue<int>("fProviderID");
            persistenceEvent.PersistenceEventTypeID = (PersistenceEventType)reader.ReadValue<int>("fPersistenceEventTypeID");
            persistenceEvent.Disabled = reader.ReadValue<bool>("fDisabled");
            persistenceEvent.Delete = reader.ReadValue<bool>("fDelete");
            persistenceEvent.Changed = reader.ReadValue<bool>("fChanged");
            persistenceEvent.Time = reader.ReadValue<DateTime>("fTime");
            persistenceEvent.Modified = reader.ReadValue<DateTime>("fModified");
            persistenceEvent.ModifiedID = reader.ReadValue<int>("fModifiedID");
            persistenceEvent.RequesterInfoID = reader.ReadValue<int>("fRequesterInfoID");
            persistenceEvent.SetContentJson(reader.ReadValue<string>("fContent"));

            return persistenceEvent;
        }
        private static List<PersistenceEventModel>GetPersistenceEventsData(IDataReader reader)
        {
            var persistenceEvent = new List<PersistenceEventModel>();
            while (reader.Read())
            {
                persistenceEvent.Add(GetPersistenceEventData(reader));
            }           
            return persistenceEvent;
        }
        private static List<RequesterInfoModel> GetRequesterInfoListData(IDataReader reader)
        {
            var requesterInfoList = new List<RequesterInfoModel>();
            while (reader.Read())
            {
                requesterInfoList.Add(new RequesterInfoModel()
                {
                    RequesterInfoID = reader.ReadValue<int>("fRequesterInfoID"),
                    AppID = reader.ReadValue<int>("fAppID"),
                    AppObjectID = reader.ReadValue<int>("fAppObjectID"),
                    AgentID = reader.ReadValue<int>("fAgentID"),
                    LocAgentID = reader.ReadValue<int>("fLocAgentID"),
                    UserID = reader.ReadValue<int>("fUserID"),
                    TerminalID = reader.ReadValue<string>("fTerminalID"),
                    TerminalUserID = reader.ReadValue<string>("fTerminalUserID"),
                    LocalTime = reader.ReadValue<DateTime>("fLocalTime"),
                    UtcTime = reader.ReadValue<DateTime>("fUtcTime"),
                    TimeZone = reader.ReadValue<string>("fTimezone"),
                    Locale = reader.ReadValue<string>("fLocale"),
                    Version = reader.ReadValue<string>("fVersion"),
                    Disabled = reader.ReadValue<bool>("fDisabled"),
                    Delete = reader.ReadValue<bool>("fDelete"),
                    Changed = reader.ReadValue<bool>("fChanged"),
                    Time = reader.ReadValue<DateTime>("fTime"),
                    Modified = reader.ReadValue<DateTime>("fModified"),
                    ModifiedID = reader.ReadValue<int>("fModifiedID"),
                });
            }
            return requesterInfoList;
        }
        private static PersistenceModel SetPersistenceID(System.Data.Common.DbParameterCollection parameters, PersistenceModel persistence)
        {
            persistence.PersistenceID = parameters.ReadValue<int>("@fPersistenceID");
            return persistence;
        }
        private static PersistenceEventModel SetPersistenceEventID(System.Data.Common.DbParameterCollection parameters, PersistenceEventModel persistenceEvent)
        {
            persistenceEvent.PersistenceEventID = parameters.ReadValue<int>("@fPersistenceEventID");      
            return persistenceEvent;
        }
        private static RequesterInfoModel SetRequesterInfoModelID(System.Data.Common.DbParameterCollection parameters, RequesterInfoModel requesterInfo)
        {
            requesterInfo.RequesterInfoID = parameters.ReadValue<int>("@fRequesterInfoID");
            return requesterInfo;
        }
     
        #endregion
    }
}