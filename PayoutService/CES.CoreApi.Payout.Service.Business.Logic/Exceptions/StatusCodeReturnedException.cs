using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Exceptions
{
    /// <summary>
    /// Custom exception
    /// Author  : David Go    
    /// </summary>
    public class StatusCodeReturnedException : Exception
    {

        int m_statusCode;
        string m_statusDescription;

        /// <summary>
        /// CONSTRUCTOR:
        /// Creates a new Exception with a message that
        /// includes the status code and descrption fields
        /// formmated in a messageText.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="statusDescription"></param>
        public StatusCodeReturnedException(int statusCode, string statusDescription)
            : base("Returned Status Code: " + statusCode + ": " + statusDescription)
        {
            m_statusCode = statusCode;
            m_statusDescription = statusDescription;
        }

        public int GetStatusCode()
        {
            return m_statusCode;
        }

        public string GetStatusDescription()
        {
            return m_statusDescription;
        }

    }
}
