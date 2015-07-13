﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CES.CoreApi.Common.Models
{
    public class PingResponseModel
    {
        public PingResponseModel()
        {
            Databases = new Collection<DatabasePingModel>();
        }

        public ICollection<DatabasePingModel> Databases { get; private set; }
    }
}
