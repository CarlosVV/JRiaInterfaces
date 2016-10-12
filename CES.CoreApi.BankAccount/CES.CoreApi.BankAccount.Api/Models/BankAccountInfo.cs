using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Models
{
    [DataContract]
    public class BankAccountInfo
    {
        [DataMember]
        public bool? IsAccountValid { get; set; }
        [DataMember(Name = "CorrespMsg")]
        public string fStatusMsg { get; set; }
        [DataMember(Name = "CorrespErrCode")]
        public string fStatusCode { get; set; }
        [DataMember]
        public string CorrespErrMsg { get; set; }
        [DataMember]
        public string BankCode { get; set; }
        [DataMember]
        public string AccountNo { get; set; }
        [DataMember(Name = "AccountName")]
        public string fBankAcctHolderName { get; set; }
        [DataMember]
        public string BankName { get; set; }
        [DataMember(Name = "ProcessorID")]
        public int fProcessorID { get; set; }
        [DataMember]
        public int BankID { get; set; }
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDesc { get; set; }
        [DataMember]
        public string Provider { get; set; }
        [DataMember(Name = "RqstDate")]
        public string fRequested { get; set; }
        [DataMember]
        public string AccountValid
        {
            get
            {
                string sAuto = "false";

                if (!IsAccountValid.HasValue) sAuto = "unknown";
                else if (IsAccountValid.Value) sAuto = "true";

                return sAuto;
            }
            set { }
        }

        public BankAccountInfo()
        {
            //IsAccountValid = false;
            fStatusMsg = "";
            fStatusCode = "";
            CorrespErrMsg = "";

            BankCode = "";
            AccountNo = "";
            fBankAcctHolderName = "";

            Provider = "";
            ErrorCode = 0;
            ErrorDesc = "";
        }

        public BankAccountInfo(int nProcessorID, string sProvider, string sBankCode, string sBankName, string sAccountNo)
        {
            fProcessorID = nProcessorID;
            fStatusMsg = "";
            fStatusCode = "";
            CorrespErrMsg = "";

            BankCode = sBankCode;
            BankName = sBankName;
            AccountNo = sAccountNo;
            fBankAcctHolderName = "";

            Provider = sProvider;
            ErrorCode = 0;
            ErrorDesc = "";
        }

        public BankAccountInfo(ErrorCodes nErrCode, string sErrDesc, int nProcessorID, string sBankName, string sBankCode, string sAccountNo)
        {
            BankCode = sBankCode;
            BankName = sBankName;
            AccountNo = sAccountNo;
            fProcessorID = nProcessorID;

            //IsAccountValid = false;
            fStatusMsg = "";
            CorrespErrMsg = "";
            CorrespErrMsg = "";

            Provider = "";
            ErrorCode = (int)nErrCode;
            ErrorDesc = sErrDesc;
        }

        public bool IsRequestCompleted()
        {
            bool bResult = (ErrorCode == 0);

            return bResult;
        }
    }
}
