using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Configuration.Model.Models;

namespace CES.CoreApi.Configuration.Web.Controllers
{
    public class SettingController : ApiController
    {
        private readonly ISettingManager _manager;

        #region Core

        public SettingController(ISettingManager manager)
        {
            if (manager == null) throw new ArgumentNullException("manager");
            _manager = manager;
        }

        #endregion

        public IEnumerable<SettingModel> Get(int serviceId)
        {
            return _manager
                .GetList(serviceId)
                .OrderBy(p => p.Name);
        }

        public HttpResponseMessage Post([FromBody] SettingModel setting)
        {
            if (ModelState.IsValid)
            {
                _manager.Update(setting);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Successfully updated.")
                };
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}