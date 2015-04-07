using System.Collections.Generic;
using CES.CoreApi.Configuration.Model.DomainEntities;

namespace CES.CoreApi.Configuration.Model.Interfaces
{
    public interface IServicesRepository
    {
        IEnumerable<Service> GetList();
    }
}