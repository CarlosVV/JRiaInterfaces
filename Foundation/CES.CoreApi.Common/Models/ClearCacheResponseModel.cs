using CES.CoreApi.Common.Constants;

namespace CES.CoreApi.Common.Models
{
    public class ClearCacheResponseModel
    {
        public string Message
        {
            get
            {
                return IsOk
                    ? OtherConstants.ClearCacheMessageSuccess
                    : OtherConstants.ClearCacheMessageFailure;
            }
        }

        public bool IsOk { get; set; }
    }
}