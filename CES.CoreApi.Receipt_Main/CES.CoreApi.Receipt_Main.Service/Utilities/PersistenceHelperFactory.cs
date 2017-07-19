using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Shared.Persistence.Business;
using CES.CoreApi.Shared.Persistence.Data;
using CES.CoreApi.Shared.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public class PersistenceHelperFactory
    {
        public static IPersistenceHelper GetPersistenceHelper()
        {
            if (AppSettings.IsStandAloneApplication)
            {
                return new StandalonePersistence();
            }

            return new PersistenceHelper(new PersistenceRepository());
        }
    }
}