using System.Configuration;
namespace CES.CoreApi.BankAccount.Api.Utilities
{
    internal static class AppSettings
    {
        internal static int ToNumber(this string value)
        {
            int num;
            int.TryParse(value, out num);
            return num;
        }

        internal static int AppId
        {
            get { return ConfigurationManager.AppSettings["ApplicationId"].ToNumber(); }
        }
        internal static int AppObjectId
        {
            get { return ConfigurationManager.AppSettings["AppObjectId"].ToNumber(); }
        }
    }
}