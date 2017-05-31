using CES.Caching;
using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CES.CoreApi.Receipt_Main.Service.Repositories
{
    public class CodesRepositoryCached: CodesRepository
    {

        public override IEnumerable<CoreApiCode> GetCoreApiCodesFromProviderByType(int providerID, string codeType)
        {

            var key = $"GetCodes_AppID_{AppSettings.AppId}_ProviderID_{providerID}_CodeType_{codeType}";
            Logging.Log.Info($"Get CoreApiCodes from Cache. Key: {key}"); 
            var data = Cache.Get<IEnumerable<CoreApiCode>>(key, null);

            if (!data?.Any() == null)
            {
                Logging.Log.Info("Get CoreApiCodes from Database.");
                data = base.GetCoreApiCodesFromProviderByType(providerID, codeType);
                Cache.Add(key, data, new TimeSpan(1, 0, 0));
                return data;
            }
            return data;
        }
    }
}
