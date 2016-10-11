using CES.Caching;
using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.Models.DTO;
using System;
using System.Collections.Generic;

namespace CES.CoreApi.Compliance.Screening.Repositories
{
    public class WatchlistRuleRepositoryCached : WatchlistRuleRepository
    {
      
        public override IEnumerable<Constant> GetEntryTypes()
        {

            var key = "GetSystblConst2Values";
            IEnumerable<Constant> data = null;// Cache.Get<IEnumerable<Constant>>(key, null);
            if (data == null)
            {
                Logging.Log.Info("Get EntryTypes from Database.");
                data = base.GetEntryTypes();
                //Cache.Add(key, data, new TimeSpan(2, 0, 0)); //TODO: Remove for production
            }
            {
                Logging.Log.Info($"Get EntryTypes from Cache. Key: {key}");
            }
            return data;

        }

       
    }
}