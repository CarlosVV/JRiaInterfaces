using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.CallLog.Shared
{
    public delegate void RequestDelegate(string requestBody, out Guid guid, string url);
    public delegate void ReplyDelegate(string replyBody, Guid guid, string url);
}
