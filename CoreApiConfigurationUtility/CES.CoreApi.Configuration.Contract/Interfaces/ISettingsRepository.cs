using System.Collections.Generic;
using CES.CoreApi.Configuration.Model.DomainEntities;

namespace CES.CoreApi.Configuration.Model.Interfaces
{
    public interface ISettingsRepository
    {
        IEnumerable<Setting> GetList(int serviceApplicationId);
        void Update(Setting setting);
    }
}