using System;
using System.Collections.Generic;
using FluentValidation;
using IValidatorFactory = CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces.IValidatorFactory;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Factories
{
    public class ValidatorFactory : Dictionary<string, Func<IValidator>>, IValidatorFactory
    {
        public T GetInstance<T>() where T : class
        {
            var name = typeof (T).Name;
            return this[name]() as T;
        }

        public IValidator GetInstance(string name)
        {
            return this[name]();
        }

        public IEnumerable<string> RegisteredValidators()
        {
            return Keys;
        }
    }
}