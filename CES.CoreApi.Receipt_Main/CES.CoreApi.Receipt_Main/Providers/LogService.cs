using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Services
{
    public class LogService
    {
        public static readonly Log4NetProxy _logger = new Log4NetProxy();
        public void LogInfoObjectToJson(dynamic objectData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(objectData, Formatting.Indented,
                                                                               new JsonSerializerSettings
                                                                               {
                                                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                                                   NullValueHandling = NullValueHandling.Ignore
                                                                               });
                LogInfo(json);
            }
            catch (Exception e)
            {

                LogWarning("Could not log LogInfoObjectToJson: " + e.Message);

            }

        }

        public void LogInfoObjectToJson(string textDescription, dynamic objectData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(objectData, Formatting.Indented,
                                                                               new JsonSerializerSettings
                                                                               {
                                                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                                                   NullValueHandling = NullValueHandling.Ignore
                                                                               });
                LogInfo($"{textDescription}: {json}");
            }
            catch (Exception e)
            {

                LogWarning("Could not log LogInfoObjectToJson: " + e.Message);

            }

        }

        public void LogInfo(string message)
        {
            Logging.Log.Info(message);
        }

        public void LogWarning(string message)
        {
            Logging.Log.Warning(message);
        }

        public void LogDebug(string message)
        {
            Logging.Log.Debug(message);
        }

        public void LogError(string message)
        {
            Logging.Log.Error(message);
        }
    }
}