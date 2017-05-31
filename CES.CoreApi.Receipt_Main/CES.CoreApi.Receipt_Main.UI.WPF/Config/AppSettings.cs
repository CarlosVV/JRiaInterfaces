using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Config
{
    public static class AppSettings
    {
        public static string DbPath
        {
            get
            {
                var dbPath = ConfigurationManager.AppSettings["dbpath"];
                if(dbPath == null)
                {
                    dbPath = @".\Database\";
                }
                return dbPath;
            }
        }

        public static string ApiReceiptServiceUrl
        {
            get
            {
                var apiReceiptServiceUrl = ConfigurationManager.AppSettings["ApiReceiptServiceUrl"];
                if (apiReceiptServiceUrl == null)
                {
                    apiReceiptServiceUrl = @"http://localhost:48712/";
                }
                return apiReceiptServiceUrl;
            }
        }
    }
}
