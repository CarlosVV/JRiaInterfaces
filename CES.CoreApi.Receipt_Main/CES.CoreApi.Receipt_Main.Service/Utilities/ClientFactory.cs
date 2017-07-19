using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public class ClientFactory
    {
        public static IClient GetClient()
        {
            if (AppSettings.IsStandAloneApplication)
            {
                return new StandaloneClient();
            }

            return new Client();
        }
    }
}