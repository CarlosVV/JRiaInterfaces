using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Models.DTOs;
using CES.CoreApi.BankAccount.Api.Repositories;
using CES.CoreApi.BankAccount.Api.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.BankAccount.Service.Business.Logic.Processors
{
    
    public class BankAccountValidationProcessor
    {
        private BankAccountRespositoryCached _repository;
      
        private string _clientAppId = "";
        public BankAccountValidationProcessor()
        {
            _repository = new BankAccountRespositoryCached();
        }


        internal ValidateBankAccountInfo ValidateBankDeposit(ValidateBankDepositRequest bankDepositRequest)
        {
            //throw new System.NotImplementedException();

            var respModel = _repository.RunBankDepositValidate(bankDepositRequest);


            if (respModel.DbResult == null) throw new Exception("No data return from SP bank deposit validation");

            if (respModel.DbResult.ProviderID > 0)
            {
                List<Processor> arProcessor = _repository.GetProcessor(respModel.DbResult.ProviderID, bankDepositRequest.BankID, bankDepositRequest.BankCountryID).ToList();

                string sBankName = string.Empty; 
                string sBankCode = _repository.GetBankcode(bankDepositRequest.BankID);
                BankAccountInfo resp = null;

                if (arProcessor == null || arProcessor.Count < 1) respModel.Message += "no validation processor found;";
                else
                {
                    for (int i = 0; i < arProcessor.Count; i++)
                    {
                        resp = ValidateBankAccount(arProcessor[i].fProcessorID, bankDepositRequest.BankID, respModel.DbResult.CorrespID, sBankName, sBankCode, bankDepositRequest.BankAccountNo, false);
                        if (resp.IsRequestCompleted()) break;//add function to check if request success, valid or invalid
                    }
                }

                respModel.CorrespInfo = resp;
            }



            return respModel;
        }

        private BankAccountInfo ValidateBankAccount(int nProcessorID, int nBankID, int nPayAgentID, string sBankName, string sBankCode, string sAccountNumber, bool bMatchProcessor)
        {
            string auto;
            string sClientAppId = getClientAppId();
            _clientAppId = sClientAppId;

            Logging.Log.Info(string.Format("processing, Cient ApplicationId:{0}", sClientAppId));

            if (string.IsNullOrWhiteSpace(sBankCode)) sBankCode = _repository.GetBankcode(nBankID);

            BankAccountInfo resp = validateRequest(nProcessorID, nPayAgentID, sBankCode, sAccountNumber);
            if (resp != null) return resp;

            Hashtable htSetting = null, htPaSetting = null;

            try
            {
                htSetting = GetSetting();

                int nDaysExpired = 0;
                if (htSetting != null)
                {
                    auto = TextUtil.GetStr(htSetting["AcctInfo_Expired_Days"]);
                    int.TryParse(auto, out nDaysExpired);
                }
                if (nDaysExpired > 0 && (!bMatchProcessor))
                {
                    resp = _repository.GetBankAcctInfo(bMatchProcessor ? nProcessorID : 0, nBankID, sBankName, sBankCode, sAccountNumber, nDaysExpired);
                    if (resp != null) return resp;
                }

                if (nProcessorID != 0) htPaSetting = _repository.GetProcessorInfo(nProcessorID);
                else htPaSetting = GetPaSetting(nPayAgentID);

                if (htPaSetting == null) return new BankAccountInfo(ErrorCodes.CorrespNotSetup, "Processor not setup", nPayAgentID, sBankName, sBankCode, sAccountNumber);

                auto = TextUtil.GetStr(htPaSetting["Disabled"]).ToLower();
                if (auto == "1" || auto == "true") return new BankAccountInfo(ErrorCodes.CorrespDisabled, "Corresp disabled", nPayAgentID, sBankName, sBankCode, sAccountNumber);

                string sName = TextUtil.GetStr(htPaSetting["Name"]);

                string sClassName = TextUtil.GetStr(htPaSetting["ClassName"]);
                if (string.IsNullOrWhiteSpace(sClassName)) throw new Exception("ClassName not defined");

                if (!sClassName.StartsWith("CES.")) sClassName = "CES.CoreApi.BankAccount.Service.Business.Logic.Processors" + "." + sClassName;
                object obj = Activator.CreateInstance(Type.GetType(sClassName));

                _repository.GetProcessorSetting(nProcessorID, htPaSetting);
                BaseBaProcessor oProcessor = (BaseBaProcessor)obj;
                oProcessor.BaProcessor = this;
                oProcessor.ProcessorID = nProcessorID;
                oProcessor.PaAgentID = nPayAgentID;
                oProcessor.PaSetting = htPaSetting;
                oProcessor.Initialize();

                resp = oProcessor.ValidateAccount(sName, sBankName, sBankCode, sAccountNumber);

                if (resp != null && resp.Provider != "Cache")
                {
                    resp.fRequested = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    resp.BankID = nBankID;
                    if (resp.IsAccountValid == true && nBankID > 0) _repository.SaveBankAcctInfo(resp);
                }
            }
            catch (Exception ex)
            {
                resp = new BankAccountInfo(ErrorCodes.ExceptionValidateBankAcct, "Exception at ValidateBankAccount", nPayAgentID, sBankName, sBankCode, sAccountNumber);
                if (htPaSetting != null && htPaSetting.ContainsKey("Name")) resp.Provider = htPaSetting["Name"].ToString();
                resp.ErrorDesc = "Exception|" + ex.ToString() + "|loc:ValidateBankAccount";
                //TDDO: LogError("9901|" + resp.ErrorCode);
            }

            return resp;
        }

        private BankAccountInfo validateRequest(int nProcessorID, int nPayAgentID, string sBankName, string sAccountNumber)
        {
            BankAccountInfo resp = null;
            string sErr = "";

            if (nProcessorID <= 0) sErr += "Processor ID must be greater than 0;";
            if (String.IsNullOrWhiteSpace(sAccountNumber)) sErr += "Account number is required;";

            if (sErr.Length > 0)
            {
                resp = new BankAccountInfo(ErrorCodes.InvalidRequest, "Invalid request:" + sErr, nPayAgentID, sBankName, "", sAccountNumber);
            }

            return resp;
        }
      
        private Hashtable GetSetting()
        {
            Hashtable ht = _repository.GetProcessorInfo(0);
            if (ht != null)
            {
                _repository.GetProcessorSetting(0, ht);
                string auto = TextUtil.GetStr(ht["Disabled"]).ToLower();
                if (auto == "1" || auto == "true") return null;
            }

            return ht;
        }

        private Hashtable GetPaSetting(int nPayAentID)
        {
            string sName = "PASetting_" + nPayAentID.ToString();
            string sValue = _repository.GetCoreAPISettings(sName);
            if (string.IsNullOrWhiteSpace(sValue)) return null;
            Hashtable ht = Newtonsoft.Json.JsonConvert.DeserializeObject<Hashtable>(sValue);
            return ht;
        }

        private string getClientAppId()
        {
            return Client.Identity.ApplicationId.ToString();
        }
    }

  
}