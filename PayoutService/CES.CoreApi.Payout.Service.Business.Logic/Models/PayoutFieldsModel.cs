using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class PayoutFieldsModel
    {
        public int FieldID { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public bool FieldRequired { get; set; }
        public bool DataExists { get; set; }
        public bool DataInvalid { get; set; }
        public string NoteOnData { get; set; }
        public string DataErrorCode { get; set; }

    }
}
