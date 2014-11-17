using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IHostApplicationProvider
    {
        /// <summary>
        /// Validates host service application
        /// 1. ApplicationId defined in config file
        /// 2. ServerId defined in config file
        /// 3. Application exist
        /// 4. Application is active itself
        /// 5. Application is active on server defined by serverId
        /// </summary>
        IHostApplication GetApplication();
    }
}