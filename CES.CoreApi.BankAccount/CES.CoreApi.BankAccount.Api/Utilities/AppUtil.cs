using CES.CoreApi.BankAccount.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.BankAccount.Api.Utilities
{
    class AppUtil
    {
        private static bool _initialized = false;
        public static int AppID { get; set; }
        public static string ConStr_Main { get; set; }

        static AppUtil()
        {
            Init();
            InitSB_PGP();
        }

        public static void InitSB_PGP()
        {
            try
            {
                string[] sLic = new string[1];
                sLic[0] = GetPGPLic();
                CES.CoreApi.Crypto.PGP.Initialize(sLic, GetConStringMain());
            }
            catch { }
        }

        public static void Init()
        {
            if (_initialized) return;
            AppID = GetAppID();
            var connection = DatabaseName.GetConnectionString(DatabaseName.Main);
            ConStr_Main = GetConStringMain();
            _initialized = true;
        }

        public static int GetAppID()
        {
            int nAppID;

            string sAppID = System.Configuration.ConfigurationManager.AppSettings["ApplicationID"];
            int.TryParse(sAppID, out nAppID);

            return nAppID;
        }


        public static string GetConStringMain()
        {//main
            var connection = DatabaseName.GetConnectionString(DatabaseName.Main);
            return connection.ConnectionString;
            //string conStr = "";

            //try
            //{
            //    CES.CoreApi.Foundation.Data.Configuration.DataAccessConfiguration dc = (CES.CoreApi.Foundation.Data.Configuration.DataAccessConfiguration)ConfigurationManager.GetSection("dataAccessConfiguration");
            //    conStr = dc.ServerGroups[0][0].ConnectionString;//search for group:main, server:main
            //    for (int i = 0; i < dc.ServerGroups.Count; i++)
            //    {
            //        if (dc.ServerGroups[i].Name.ToLower() != sGroupName.ToLower()) continue;
            //        for (int j = 0; j < dc.ServerGroups[i].Count; j++)
            //        {
            //            if (dc.ServerGroups[i][j].Name.ToLower() == sServerName.ToLower())
            //            {
            //                conStr = dc.ServerGroups[i][j].ConnectionString;
            //                break;
            //            }
            //        }

            //        if (conStr != "") break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string auto = ex.Message;
            //}

            //return conStr;
        }

        public static string GetPGPLic()
        {
            string sLic = System.Configuration.ConfigurationManager.AppSettings["SB_PGP_LicenseKey"];

            return sLic;
        }

        public static double GetSettingTOMin()
        {
            double nOtMin;

            string auto = System.Configuration.ConfigurationManager.AppSettings["SettingTOMin"];
            if (string.IsNullOrWhiteSpace(auto)) nOtMin = 10.0;
            else double.TryParse(auto, out nOtMin);

            return nOtMin;
        }
    }
}