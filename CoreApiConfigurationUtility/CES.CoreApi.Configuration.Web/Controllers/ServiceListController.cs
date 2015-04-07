using System;
using System.Collections.Generic;
using System.Web.Http;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Configuration.Model.Models;

namespace CES.CoreApi.Configuration.Web.Controllers
{
    public class ServiceListController: ApiController
    {
        private readonly IServiceManager _manager;

        public ServiceListController(IServiceManager manager)
        {
            if (manager == null) throw new ArgumentNullException("manager");
            _manager = manager;
        }

        public IEnumerable<ServiceModel> Get()
        {
            return _manager.GetList();
        }
    }
}