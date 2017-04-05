using System.Configuration;
namespace CES.CoreApi.Receipt_Main.Utilities
{
    internal static class AppSettings
    {
        internal static int ToNumber(this string value)
        {
            int num;
            int.TryParse(value, out num);
            return num;
        }

        internal static bool ToBoolean(this string value)
        {
            bool boolean;
            bool.TryParse(value, out boolean);
            return boolean;
        }

        internal static int AppId
        {
            get { return ConfigurationManager.AppSettings["ApplicationId"].ToNumber(); }
        }
        internal static int AppObjectId
        {
            get { return ConfigurationManager.AppSettings["AppObjectId"].ToNumber(); }
        }
        internal static bool EnableSelfSignedSSLCert
        {
            get { return ConfigurationManager.AppSettings["EnableSelfSignedSSLCert"].ToBoolean(); }
        }
    }
}