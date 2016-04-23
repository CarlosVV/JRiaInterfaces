using System;
using System.Collections.Generic;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Configuration.Model.Models;

namespace CES.CoreApi.Configuration.Business.Services
{
	public class ServiceManager : IServiceManager
    {
      //  private readonly IServicesRepository _repository;
       // private readonly IAutoMapperProxy _mapper;

        public ServiceManager(IServicesRepository repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            //if (mapper == null) throw new ArgumentNullException("mapper");

            //_repository = repository;
            //_mapper = mapper;
        }

        public IEnumerable<ServiceModel> GetList()
        {
           // var services = _repository.GetList();
         //   var serviceModelList = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceModel>>(services);
            return null;
        }
    }
}
