//using System;
//using System.ServiceModel;
//using System.ServiceModel.Channels;
//using System.ServiceModel.Description;
//using System.ServiceModel.Dispatcher;
//using SimpleInjector;

//namespace CES.CoreApi.Foundation.Service
//{
//    public class IocBasedInstanceProvider : IInstanceProvider, IContractBehavior
//    {
//        #region Core

//        private readonly Type _serviceType;
//        private readonly Container _container;

//        public IocBasedInstanceProvider(Container container, Type serviceType)
//        {
//            if (container == null) 
//                throw new ArgumentNullException("container");
//            if (serviceType == null) 
//                throw new ArgumentNullException("serviceType");

//            _container = container;
//            _serviceType = serviceType;
//        }

//        #endregion

//        #region IInstanceProvider Members

//        public object GetInstance(InstanceContext instanceContext, Message message)
//        {
//            return GetInstance(instanceContext);
//        }

//        public object GetInstance(InstanceContext instanceContext)
//        {
//            return _container.GetInstance(_serviceType);
//        }

//        public void ReleaseInstance(InstanceContext instanceContext, object instance)
//        {
//            var disposable = instance as IDisposable;
//            if (disposable != null)
//                disposable.Dispose();
//        }

//        #endregion

//        #region IContractBehavior Members

//        public void AddBindingParameters(ContractDescription contractDescription,
//            ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
//        {
//        }

//        public void ApplyClientBehavior(ContractDescription contractDescription,
//            ServiceEndpoint endpoint, ClientRuntime clientRuntime)
//        {
//        }

//        public void ApplyDispatchBehavior(ContractDescription contractDescription,
//            ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
//        {
//            dispatchRuntime.InstanceProvider = this;
//        }

//        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
//        {
//        }

//        #endregion
//    }
//}