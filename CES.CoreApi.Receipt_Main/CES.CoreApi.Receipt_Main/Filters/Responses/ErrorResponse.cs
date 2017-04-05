using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Filters.Responses
{
    public class ErrorResponse
    {
        public long PersistenceID { get; set; }
        public List<Error> Errors { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public Guid ResponseId { get; private set; }
        public ErrorResponse(string message, int resultCode, List<Error> errors, Guid responseId, long persistenceID)
        {
            Errors = errors;
            Message = message;
            ResultCode = resultCode;
            ResponseId = responseId;
            PersistenceID = persistenceID;
        }
    }
}