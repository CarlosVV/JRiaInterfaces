using System.Collections.Generic;
using FluentValidation;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces
{
    public interface IValidatorFactory
    {
        T GetInstance<T>() where T : class;
        IEnumerable<string> RegisteredValidators();
        IValidator GetInstance(string name);
    }
}