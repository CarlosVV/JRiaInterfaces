using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Models.DTOs;
using CES.CoreApi.BankAccount.Api.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.BankAccount.Api.Repositories
{
    internal class BankAccountRespositoryCached:BankAccountRespository
    {
        public override IEnumerable<CurrencyCountry> GetServiceOfferdCurrencies(CountryCurrencyRequest request)
        {

            var key = $"GetServiceOfferdCurrencies_{request.CountryFrom}{request.CountryTo}";

            return Caching.Cache.Get(key, () => base.GetServiceOfferdCurrencies(request));
        }
        public override Hashtable GetProcessorInfo(int nProcessorID)
        {
            var key = $"PS_{nProcessorID}";
            Hashtable data = Caching.Cache.Get<Hashtable>(key, null);
            if (data == null)
            {
                data = base.GetProcessorInfo(nProcessorID);
                Caching.Cache.Add(key, data, new TimeSpan(2, 0, 0));
            }
            return data;
        }
        public override Dictionary<string, string> GetCoreAPISettings_App()
        {
            var key = $"CoreAPISetting_{AppSettings.AppId}_{AppSettings.AppObjectId}";
            Dictionary<string, string> data = Caching.Cache.Get<Dictionary<string, string>>(key, null);
            if (data == null)
            {
                data = base.GetCoreAPISettings_App();
                Caching.Cache.Add(key, data, new TimeSpan(2, 0, 0));
            }
            return data;
        }
        public override IEnumerable<Processor> GetProcessor(int nProviderMapID, int nBankID, int nBankCounteyID)
        {
            return base.GetProcessor(nProviderMapID, nBankID, nBankCounteyID);
        }
        public override string GetBankcode(int nBankID)
        {
            return base.GetBankcode(nBankID);
        }
        public override void GetProcessorSetting(int nProcessorID, Hashtable ht)
        {
            var key = $"PS_{nProcessorID}";
            Hashtable data = Caching.Cache.Get<Hashtable>(key, null);
            if (data == null)
            {
                base.GetProcessorSetting(nProcessorID, ht);
                Caching.Cache.Add(key, data, new TimeSpan(2, 0, 0));
            }

        }
        public string GetCoreAPISettings(string sName)
        {
            Dictionary<string, string> caSetting = GetCoreAPISettings_App();

            string sValue = TextUtil.GetStr(caSetting[sName]);

            return sValue;
        }

        public override void SaveBankAcctInfo(BankAccountInfo baInfo)
        {
            base.SaveBankAcctInfo(baInfo);
        }
        public override CPaConn GetPaWsSetting(int payAgentID)
        {
            return base.GetPaWsSetting(payAgentID);
        }

        public override Hashtable GetPaExportSetting(int nExportFormatID)
        {
            return base.GetPaExportSetting(nExportFormatID);
        }

       
    }
}