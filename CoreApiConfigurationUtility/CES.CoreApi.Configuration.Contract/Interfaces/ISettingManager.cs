using System.Collections.Generic;
using CES.CoreApi.Configuration.Model.Models;

namespace CES.CoreApi.Configuration.Model.Interfaces
{
    public interface ISettingManager
    {
        IEnumerable<SettingModel> GetList(int serviceId);
        void Update(SettingModel settingModel);
    }
}