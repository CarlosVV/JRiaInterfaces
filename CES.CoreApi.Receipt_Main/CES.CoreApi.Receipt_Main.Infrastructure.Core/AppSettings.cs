using System.Configuration;
using System.Web.Hosting;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Core
{
    public static class AppSettings
    {
        public static int ToNumber(this string value)
        {
            int num;
            int.TryParse(value, out num);
            return num;
        }

        public static bool ToBoolean(this string value)
        {
            bool boolean;
            bool.TryParse(value, out boolean);
            return boolean;
        }

        public static int AppId
        {
            get { return ConfigurationManager.AppSettings["ApplicationId"].ToNumber(); }
        }
        internal static int AppObjectId
        {
            get { return ConfigurationManager.AppSettings["AppObjectId"].ToNumber(); }
        }
        public static bool EnableSelfSignedSSLCert
        {
            get { return ConfigurationManager.AppSettings["EnableSelfSignedSSLCert"].ToBoolean(); }
        }
        public static string RedisConnectionString
        {
            get { return ConfigurationManager.AppSettings["redisConnection"]; }
        }
        public static bool UseLocalStorageForStores
        {
            get { return ConfigurationManager.AppSettings["UseLocalStorageForStores"] == null || ConfigurationManager.AppSettings["UseLocalStorageForStores"].ToBoolean(); }
        }
        public static string ApplicationBinPath
        {
            get {return $"{HostingEnvironment.ApplicationPhysicalPath}\\bin";}
        }

        public static bool IsStandAloneApplication
        {
            get { return System.Diagnostics.Process.GetCurrentProcess().ProcessName.Contains("CES.CoreApi.Receipt_Main.UI.WPF"); }
        }
    }
}