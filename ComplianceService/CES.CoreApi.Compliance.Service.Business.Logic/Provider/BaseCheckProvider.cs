using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Business.Logic.Provider
{
    public abstract class BaseCheckProvider
    {
        #region Core

        protected BaseCheckProvider(CheckPayoutProviderType providerType)
        {
            ProviderType = providerType;
        }

        #endregion

        #region Protected methods


        protected CheckPayoutProviderType ProviderType { get; private set; }


        #endregion
    }
}
