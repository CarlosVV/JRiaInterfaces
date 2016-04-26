using System;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;

namespace CES.CoreApi.Wcf.Factory.Service
{
	public class IocBasedServiceHost : SimpleInjectorServiceHost
	{
	
	
		public IocBasedServiceHost(Container container, Type serviceType, params Uri[] baseAddresses)
			: base(container, serviceType, baseAddresses)
		{
			
		}


	}
}
