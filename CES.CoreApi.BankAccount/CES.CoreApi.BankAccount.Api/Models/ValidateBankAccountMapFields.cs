using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Models
{
    public class ValidateBankAccountMapFields
    {
        public string rst_Fields { get; set; }
        public int FieldID { get; set; }
        public string FieldName { get; set; }
        public int LenMin { get; set; }
        public int LenMax { get; set; }
        public bool bPresent { get; set; }
        public bool bInvalid_Length { get; set; }
        public bool bInvalid_Data { get; set; }
        public string CorrectedData { get; set; }
    }
}
