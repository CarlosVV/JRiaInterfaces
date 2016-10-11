using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Model
{
    public class PersistenceEventModel:BaseModel
    {
        private dynamic _contentObject = null;
        private string _jsonContent { get; set; }
        private RequesterInfoModel _requesterInfoModel = null;
        public PersistenceEventModel(int persistenceID,int providerID, PersistenceEventType persistenceEventTypeID)        {
            
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
        public int PersistenceID { get; set; }
        public int AppID { get; set; }
        public int ProviderID { get; set; }
        public PersistenceEventType PersistenceEventTypeID { get; set; }
        public int RequesterInfoID { get; set; }
        public RequesterInfoModel RequesterInfo
        {
            get
            {
                return _requesterInfoModel;
            }
            set
            {
                _requesterInfoModel = value;
                if(value !=null)
                    AppID = value.AppID;
            }
        }

        public void SetContentObject<T>(object contentObject)
        {
            _contentObject =(T)contentObject;
        }

        public void SetContentJson(string contentJson)
        {
            _jsonContent = contentJson;
        }

        public string GetPersistenceJson()
        {
           return JsonConvert.SerializeObject(_contentObject);
        }
       

        public T GetPersistenceObject<T>()
        {
            T result = default(T);

            if (string.IsNullOrEmpty(_jsonContent)) return result;

            result =  JsonConvert.DeserializeObject<T>(_jsonContent);

            return result;

        }
    }
}
