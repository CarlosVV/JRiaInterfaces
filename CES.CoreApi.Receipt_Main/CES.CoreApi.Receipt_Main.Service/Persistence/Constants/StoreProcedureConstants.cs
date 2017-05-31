using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Constants
{
    public class StoreProcedureConstants
    {
        public const string GetPersistenceData = "coreApi_sp_PersistenceGetByID";
        public const string GetPersistenceEventData = "coreApi_sp_PersistenceEventGetByTypeID";
        public const string CreatePersistenceData = "coreApi_sp_PersistenceCreate";
        public const string CreatePresistenceEventData = "coreApi_sp_PersistenceEventCreate";
        public const string CreateRequesterInfoData = "coreApi_sp_RequestFromCreate";
        public const string UpdatePersistenceData = "coreApi_sp_PersistenceUpdate"; 

    }
}
