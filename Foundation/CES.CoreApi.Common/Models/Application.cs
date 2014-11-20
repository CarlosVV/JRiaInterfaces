using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Common.Models
{
    [DataContract]
    public class Application : IApplication
    {
        #region Core

        public Application(int id, string name, bool isActive)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id", string.Format(CultureInfo.InvariantCulture, "Invalid application ID = '{0}'", id));
            if (string.IsNullOrEmpty(name))
                throw  new ArgumentNullException("name");

            Id = id;
            Name = name;
            IsActive = isActive;
            
            Configuration = new List<ApplicationConfiguration>();
            Operations = new Collection<ServiceOperation>();
        }

        #endregion

        #region Public properties

        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public bool IsActive { get; private set; }
        
        [DataMember]
        public ICollection<ApplicationConfiguration> Configuration { get; protected set; }
        
        [DataMember]
        public ICollection<ServiceOperation> Operations { get; protected set; }

        #endregion
    }
}
