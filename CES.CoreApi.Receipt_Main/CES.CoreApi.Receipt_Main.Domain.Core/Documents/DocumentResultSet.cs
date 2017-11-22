using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public class DocumentResultSet
    {
        public MessageInfoResult MessageInfoResult { get; set; }
        public OrderInfoResult OrderInfoResult { get; set; }
    }
}
