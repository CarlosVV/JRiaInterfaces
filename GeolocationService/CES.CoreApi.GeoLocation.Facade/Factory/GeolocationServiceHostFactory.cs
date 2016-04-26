//using CES.CoreApi.GeoLocation.Facade.Configuration;
//using SimpleInjector.Integration.Wcf;
//using System;
//using System.ServiceModel;

//namespace CES.CoreApi.GeoLocation.Facade.Factory
//{
//	public class GeolocationServiceHostFactory : SimpleInjectorServiceHostFactory
//	{
//		protected override ServiceHost CreateServiceHost(Type serviceType,
//			Uri[] baseAddresses)
//		{
//			return new SimpleInjectorServiceHost(
//				Bootstrapper.Container,
//				serviceType,
//				baseAddresses);
//		}
//	}
//}
