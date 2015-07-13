using System.Collections.Generic;

namespace CES.CoreApi.Settings.Service.Contract.Interfaces
{
    public class GetIdentificationTypeListResponse
    {
        public List<IdentificationTypeResponse> IdentificationTypeList { get; set; }
    }
}