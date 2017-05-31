using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Linq;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class ReturnInfo
    {
        public ReturnInfo()
        {
            Errors = new List<ErrorModel>();
        }

        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }
        [DataMember]
        public Boolean ResultProcess { get; set; }
        public List<ErrorModel> Errors { get; set; }

    }
}