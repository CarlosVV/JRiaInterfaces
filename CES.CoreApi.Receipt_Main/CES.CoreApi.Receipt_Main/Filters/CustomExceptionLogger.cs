using System.Net.Http;
using System.Text;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Receipt_Main.ExceptionHandling
{
    /// <summary>
    /// This is a centralized expectation logger, all exception get to here  and get logged. 	//
    /// There is no need to worry about this code.Just leave it as is.  Don’t put any code in try ..catch block because all exception   handled by this logger.
    /// It logges  exception with request header message 
    /// Optional* You can customize the request message 
    /// 
    /// </summary>
    public class CustomExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            Logging.Log.Error(RequestToString(context.Request), context.Exception);
        }

        private static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Method != null)
                message.Append(request.Method);
            if (request.RequestUri != null)
                message.Append(" ").Append(request.RequestUri);
            if (request.Headers != null)
            {
                foreach (var item in request.Headers)
                {

                    message.AppendLine(" ").Append(item.Key).Append(":");
                    foreach (var str in item.Value)
                    {
                        message.Append(str).Append(" ");
                    }
                }
            }


            return message.ToString();
        }
    }
}