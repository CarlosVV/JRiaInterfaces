using System;
using System.Globalization;

namespace CES.CoreApi.Common.Models
{
    public class HostApplication: Application
    {
        #region Core

        public HostApplication(Application application, int serverId)
            :base(application.Id, application.ApplicationType, application.Name, application.IsActive)
        {
            if (serverId <= 0)
                throw new ArgumentOutOfRangeException("serverId", string.Format(CultureInfo.InvariantCulture, "Invalid server ID = '{0}'", serverId));
            ServerId = serverId;
            Operations = application.Operations;
            Servers = application.Servers;
            Configuration = application.Configuration;
        }

        #endregion

        #region Public properties

        public int ServerId { get; private set; }

        #endregion
    }
}
