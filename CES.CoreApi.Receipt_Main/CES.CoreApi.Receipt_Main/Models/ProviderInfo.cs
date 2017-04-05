using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class ProviderInfo
    {

        public int ProviderID { get; set; }
        public int ProviderTypeID { get; set; }
        public string ProviderName { get; set; }

        public ProviderInfo()
        {

        }
        //public ProviderInfo(ProviderInfo providerModel)
        //{
        //    if (providerModel != null)
        //    {
        //        ProviderID = providerModel.ProviderID;
        //        ProviderTypeID = providerModel.ProviderTypeID;
        //        ProviderName = providerModel.Name;
        //    }
        //}
    }
}