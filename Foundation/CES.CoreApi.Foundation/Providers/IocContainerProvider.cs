using System;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;

namespace CES.CoreApi.Foundation.Providers
{
    internal class IocContainerProvider
    {
        private static IIocContainer _instance;
        public void Initialize(IIocContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            Instance = container;
        }

        public static IIocContainer Instance
        {
            get
            {
                if (_instance == null)
                    throw new CoreApiException(
                        Organization.Ria, 
                        TechnicalSystem.CoreApi, 
                        TechnicalSubSystem.CoreApi, 
                        SubSystemError.ServiceIntializationIoCContainerIsNotInitialized);

                return _instance;
            }
            private set { _instance = value; }
        }

    }
}
