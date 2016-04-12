using CES.CoreApi.Foundation.Contract.Models;
using System.Collections.Generic;


namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IApplication
    {
        //[DataMember]
        int Id { get; }

       // [DataMember]
        string Name { get; }

       // [DataMember]
        bool IsActive { get; }

        //[DataMember]
        ICollection<ApplicationConfiguration> Configuration { get; }

       // [DataMember]
        ICollection<ServiceOperation> Operations { get; }
    }
}