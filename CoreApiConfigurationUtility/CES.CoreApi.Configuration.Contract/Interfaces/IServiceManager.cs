using System.Collections.Generic;
using CES.CoreApi.Configuration.Model.Models;

namespace CES.CoreApi.Configuration.Model.Interfaces
{
    public interface IServiceManager
    {
        IEnumerable<ServiceModel> GetList();
    }
}