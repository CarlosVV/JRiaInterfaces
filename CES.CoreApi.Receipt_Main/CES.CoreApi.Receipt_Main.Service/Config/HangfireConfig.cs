using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Service.Utilities;
using Hangfire;
using Hangfire.Redis.StackExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Config
{
    public static class HangfireConfig
    {
        public static void Start()
        {
            GlobalConfiguration.Configuration
                .UseRedisStorage(AppSettings.RedisConnectionString);
        }
    }
}