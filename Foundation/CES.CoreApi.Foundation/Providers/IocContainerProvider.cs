using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using SimpleInjector;

namespace CES.CoreApi.Foundation.Providers
{
    public class IocContainerProvider
    {
        private static Container _instance;
        public void Initialize(Container container)
        {
            if (container == null) 
                throw new ArgumentNullException("container");
            Instance = container;
        }

        public static Container Instance
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
