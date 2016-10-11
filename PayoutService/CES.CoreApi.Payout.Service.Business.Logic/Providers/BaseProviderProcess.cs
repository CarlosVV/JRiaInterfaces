using CES.CoreApi.Shared.Providers.Helper.Model.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers
{
   
    public abstract class BaseProviderProcess
    {
      

        #region Protected methods


        protected ProviderModel ProviderInfo { get;  set; }


        #endregion
    }
}
