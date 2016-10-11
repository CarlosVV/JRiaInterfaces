using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Utilities
{
    //TODO: This may be obsolete? Get these from DB now?
    class ConfigSettings
    {
        //TODO: DB Conns: Remove these and use CoreApi DB connection
        //DATABSE CONNECTIONS:
        public static string MainConnectionString()
        {
            return ConfigurationSettings.AppSettings["MainConnectionString"].ToString();
        }
        public static string FEConnectionString()
        {
            return ConfigurationSettings.AppSettings["FEConnectionString"].ToString();
        }
        public static string ROConnectionString()
        {
            return ConfigurationSettings.AppSettings["ROConnectionString"].ToString();
        }

        public static int CoreAPIAppID()
        {
            return Convert.ToInt32(ConfigurationSettings.AppSettings["CoreAPIAppID"].ToString());
        }
        public static int CoreAPIAppObjectID()
        {
            return Convert.ToInt32(ConfigurationSettings.AppSettings["CoreAPIAppObjectID"].ToString());
        }
        public static int PayoutServiceAppID()
        {
            return Convert.ToInt32(ConfigurationSettings.AppSettings["PayoutServiceAppID"].ToString());
        }
        public static int PayoutAppObjectID()
        {
            return Convert.ToInt32(ConfigurationSettings.AppSettings["PayoutAppObjectID"].ToString());
        }

        public static int PersistenceLifeTime()
        {
            return int.Parse(System.Configuration.ConfigurationManager.AppSettings["PersistenceLifeTime"] ?? "60");
        }
        public static int BeneficiaryNameSimilarPercent()
        {
            return int.Parse(System.Configuration.ConfigurationManager.AppSettings["BeneficiaryNameSimilarPercent"] ?? "0");
        }
        public static int SenderNameSimilarPercent()
        {
            return int.Parse(System.Configuration.ConfigurationManager.AppSettings["SenderNameSimilarPercent"] ?? "0");
        }

        public static string EmailMessageFrom()
        {
            return ConfigurationManager.AppSettings["EmailMessageFrom"].ToString();
        }
        public static string EmailMessageTo()
        {
            return ConfigurationManager.AppSettings["EmailMessageTo"].ToString();
        }


        //TODO: Remove these and use DB ones:
        //GOLDEN CROWN SETTINGS:
        //public static string GoldenCrownOrderPrefix()
        //{
        //    return ConfigurationSettings.AppSettings["GoldenCrownOrderPinPrefix"].ToString();
        //}
        //public static string GoldenCrownServerURL()
        //{
        //    return ConfigurationSettings.AppSettings["GoldenCrownServerURL"].ToString();
        //}
        //public static string GoldenCrownClientCertSubject()
        //{
        //    return ConfigurationSettings.AppSettings["GoldenCrownClientCertSubject"].ToString();
        //}
        //public static string GoldenCrownServiceCertSubject()
        //{
        //    return ConfigurationSettings.AppSettings["GoldenCrownServiceCertSubject"].ToString();
        //}
        //public static float GoldenCrownInterfaceVersion()
        //{
        //    return float.Parse(ConfigurationSettings.AppSettings["GoldenCrownInterfaceVersion"].ToString());
        //}
        //public static string GoldenCrownRiaAgentID()
        //{
        //    return ConfigurationSettings.AppSettings["GoldenCrownRiaAgentID"].ToString();
        //}

    }
}
