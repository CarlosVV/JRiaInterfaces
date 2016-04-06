﻿using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace CES.CoreApi.Security.Models
{
   // [DataContract]
    public class ApplicationServiceOperation
    {
        #region Core

        public ApplicationServiceOperation(int applicationId, bool isActive)
        {
            if (applicationId <= 0)
                throw new ArgumentOutOfRangeException("applicationId", string.Format(CultureInfo.InvariantCulture, "Invalid applicationId = '{0}'", applicationId));
            ApplicationId = applicationId;
            IsActive = isActive;
        }

        #endregion

        #region Public properties

     //   [DataMember]
        public int ApplicationId { get; set; }

       // [DataMember]
        public bool IsActive { get; set; }

        #endregion
    }
}
