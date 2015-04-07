using System;
using System.Collections.Generic;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Configuration.Model.DomainEntities;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Configuration.Model.Models;

namespace CES.CoreApi.Configuration.Business.Services
{
    public class SettingManager : ISettingManager
    {
        private readonly ISettingsRepository _repository;
        private readonly IAutoMapperProxy _mapper;

        public SettingManager(ISettingsRepository repository, IAutoMapperProxy mapper)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (mapper == null) throw new ArgumentNullException("mapper");
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<SettingModel> GetList(int serviceId)
        {
            var settings = _repository.GetList(serviceId);
            var settingModelList = _mapper.Map<IEnumerable<Setting>, IEnumerable<SettingModel>>(settings);
            return settingModelList;
        }

        public void Update(SettingModel settingModel)
        {
            var setting = _mapper.Map<SettingModel, Setting>(settingModel);
            _repository.Update(setting);
        }
    }
}
