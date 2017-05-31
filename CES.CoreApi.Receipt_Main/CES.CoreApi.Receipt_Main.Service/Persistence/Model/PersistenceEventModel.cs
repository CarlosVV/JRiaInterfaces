using CES.CoreApi.Shared.Persistence.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Model
{
    public class PersistenceEventModel : BaseModel
    {
        public dynamic ContentObject = null;
        private RequesterInfoModel _requesterInfoModel = null;
        public PersistenceEventModel(long persistenceID, int providerID, PersistenceEventType persistenceEventTypeID)
        {

            RequesterInfo = new RequesterInfoModel();
            PersistenceID = persistenceID;
            ProviderID = providerID;
            PersistenceEventTypeID = persistenceEventTypeID;
        }
        public PersistenceEventModel()
        {
            RequesterInfo = new RequesterInfoModel();
        }
        public int PersistenceEventID { get; set; }
        public long PersistenceID { get; set; }
        public int AppID { get; set; }
        public int ProviderID { get; set; }
        public PersistenceEventType PersistenceEventTypeID { get; set; }
        public int RequesterInfoID { get; set; }

        public string JsonContent { get; set; }
        public RequesterInfoModel RequesterInfo
        {
            get
            {
                return _requesterInfoModel;
            }
            set
            {
                _requesterInfoModel = value;
                if (value != null)
                    AppID = value.AppID;
            }
        }

        public void SetContentObject<T>(object contentObject)
        {
            ContentObject = (T)contentObject;
        }

        public string GetPersistenceJson()
        {
            return JsonConvert.SerializeObject(ContentObject);
        }


        public T GetPersistenceObject<T>()
        {
            T result = default(T);

            if (string.IsNullOrEmpty(JsonContent)) return result;

            result = JsonConvert.DeserializeObject<T>(JsonContent);

            return result;

        }

        public bool SaveRequestInfo { get; set; }
    }
}
