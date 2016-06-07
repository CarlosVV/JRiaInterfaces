using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace CES.CoreApi.Security.Models
{

    public class ServiceOperation
    {
        #region Core

        public ServiceOperation(int id, string name, bool isActive)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id", string.Format(CultureInfo.InvariantCulture, "Invalid ID = '{0}'", id));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            Id = id;
            Name = name;
            IsActive = isActive;
            AssignedApplications = new Collection<ApplicationServiceOperation>();
        }

        #endregion

        #region Public properties


        public int Id { get; private set; }

     
        public string Name { get; private set; }

    
        public bool IsActive { get; private set; }

     
        public ICollection<ApplicationServiceOperation> AssignedApplications { get; private set; }

        #endregion
    }
}
