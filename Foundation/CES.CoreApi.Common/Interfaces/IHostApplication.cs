using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Common.Interfaces
{
    public interface IHostApplication
    {
        int ServerId { get; }

        [DataMember]
        int Id { get; }

        [DataMember]
        string Name { get; }

        [DataMember]
        bool IsActive { get; }

        [DataMember]
        ICollection<ApplicationConfiguration> Configuration { get; }

        [DataMember]
        ICollection<ServiceOperation> Operations { get; }

        [DataMember]
        ICollection<ApplicationServer> Servers { get; }
    }
}