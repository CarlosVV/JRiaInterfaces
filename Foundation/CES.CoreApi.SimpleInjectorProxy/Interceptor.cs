using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
namespace CES.CoreApi.SimpleInjectorProxy
{


	public interface IInterceptor
	{
		void Intercept(IInvocation invocation);
	}

	public interface IInvocation
	{
		object InvocationTarget { get; }
		object ReturnValue { get; set; }
		object[] Arguments { get; }
		void Proceed();
		MethodBase GetConcreteMethod();
	}



	public static class Interceptor
	{
		public static T CreateProxy<T>(IInterceptor interceptor, T realInstance)
		{
			return (T)CreateProxy(typeof(T), interceptor, realInstance);
		}

		[DebuggerStepThrough]
		public static object CreateProxy(Type serviceType, IInterceptor interceptor,
			object realInstance)
		{
			var proxy = new InterceptorProxy(serviceType, realInstance, interceptor);
			return proxy.GetTransparentProxy();
		}

		private sealed class InterceptorProxy : RealProxy
		{
			private static MethodBase GetTypeMethod = typeof(object).GetMethod("GetType");

			private object realInstance;
			private IInterceptor interceptor;

			[DebuggerStepThrough]
			public InterceptorProxy(Type classToProxy, object realInstance,
				IInterceptor interceptor)
				: base(classToProxy)
			{
				this.realInstance = realInstance;
				this.interceptor = interceptor;
			}

			public override IMessage Invoke(IMessage msg)
			{
				if (msg is IMethodCallMessage)
				{
					var message = (IMethodCallMessage)msg;

					if (object.ReferenceEquals(message.MethodBase, GetTypeMethod))
					{
						return this.Bypass(message);
					}
					else {
						return this.InvokeMethodCall(message);
					}
				}

				return msg;
			}

			private IMessage InvokeMethodCall(IMethodCallMessage message)
			{
				var invocation =
					new Invocation { Proxy = this, Message = message, Arguments = message.Args };

				invocation.Proceeding += () => {
					invocation.ReturnValue = message.MethodBase.Invoke(
						this.realInstance, invocation.Arguments);
				};

				this.interceptor.Intercept(invocation);
				return new ReturnMessage(invocation.ReturnValue, invocation.Arguments,
					invocation.Arguments.Length, null, message);
			}

			private IMessage Bypass(IMethodCallMessage message)
			{
				object value = message.MethodBase.Invoke(this.realInstance, message.Args);

				return new ReturnMessage(value, message.Args, message.Args.Length, null, message);
			}

			private class Invocation : IInvocation
			{
				public event Action Proceeding;
				public InterceptorProxy Proxy { get; set; }
				public object[] Arguments { get; set; }
				public IMethodCallMessage Message { get; set; }
				public object ReturnValue { get; set; }

				public object InvocationTarget
				{
					get { return this.Proxy.realInstance; }
				}

				public void Proceed()
				{
					this.Proceeding();
				}

				public MethodBase GetConcreteMethod()
				{
					return this.Message.MethodBase;
				}
			}
		}
	}
}
