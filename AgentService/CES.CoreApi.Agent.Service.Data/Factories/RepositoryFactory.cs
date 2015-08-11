using System;
using System.Collections.Generic;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Base;

namespace CES.CoreApi.Agent.Service.Data.Factories
{
    public class RepositoryFactory : Dictionary<string, Func<BaseGenericRepository>>, IRepositoryFactory
    {
        public T GetInstance<T>() where T : class
        {
            var name = typeof(T).Name;
            return this[name]() as T;
        }
    }
}
