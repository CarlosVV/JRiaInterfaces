//using System;
//using System.Diagnostics;
//using System.Linq.Expressions;
//using Castle.DynamicProxy;
//using SimpleInjector;

//// Extension methods for interceptor registration
//// NOTE: These extension methods can only intercept interfaces, not abstract types.
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

using SimpleInjector;
namespace CES.CoreApi.SimpleInjectorProxy
{
	//    public static class InterceptorExtensions
	//    {
	//        #region public methods

	//        public static void InterceptWith<TInterceptor>(this Container container, Func<Type, bool> predicate) 
	//            where TInterceptor : class, IInterceptor
	//        {
	//            RequiresIsNotNull(container, "container");
	//            RequiresIsNotNull(predicate, "predicate");
	//            container.Options.ConstructorResolutionBehavior.GetConstructor(typeof(TInterceptor), typeof(TInterceptor));

	//            var interceptWith = new InterceptionHelper(container)
	//            {
	//                BuildInterceptorExpression = e => BuildInterceptorExpression<TInterceptor>(container),
	//                Predicate = predicate
	//            };

	//            container.ExpressionBuilt += interceptWith.OnExpressionBuilt;
	//        }

	//        public static void InterceptWith(this Container container, Func<IInterceptor> interceptorCreator, Func<Type, bool> predicate)
	//        {
	//            RequiresIsNotNull(container, "container");
	//            RequiresIsNotNull(interceptorCreator, "interceptorCreator");
	//            RequiresIsNotNull(predicate, "predicate");

	//            var interceptWith = new InterceptionHelper(container)
	//            {
	//                BuildInterceptorExpression = e => Expression.Invoke(Expression.Constant(interceptorCreator)),
	//                Predicate = predicate
	//            };

	//            container.ExpressionBuilt += interceptWith.OnExpressionBuilt;
	//        }

	//        public static void InterceptWith(this Container container, Func<ExpressionBuiltEventArgs, IInterceptor> interceptorCreator, Func<Type, bool> predicate)
	//        {
	//            RequiresIsNotNull(container, "container");
	//            RequiresIsNotNull(interceptorCreator, "interceptorCreator");
	//            RequiresIsNotNull(predicate, "predicate");

	//            var interceptWith = new InterceptionHelper(container)
	//            {
	//                BuildInterceptorExpression = e => Expression.Invoke(Expression.Constant(interceptorCreator), Expression.Constant(e)),
	//                Predicate = predicate
	//            };

	//            container.ExpressionBuilt += interceptWith.OnExpressionBuilt;
	//        }

	//        public static void InterceptWith(this Container container, IInterceptor interceptor, Func<Type, bool> predicate)
	//        {
	//            RequiresIsNotNull(container, "container");
	//            RequiresIsNotNull(interceptor, "interceptor");
	//            RequiresIsNotNull(predicate, "predicate");

	//            var interceptWith = new InterceptionHelper(container)
	//            {
	//                BuildInterceptorExpression = e => Expression.Constant(interceptor),
	//                Predicate = predicate
	//            };

	//            container.ExpressionBuilt += interceptWith.OnExpressionBuilt;
	//        }

	//        #endregion

	//        #region Private methods

	//        [DebuggerStepThrough]
	//        private static Expression BuildInterceptorExpression<TInterceptor>(Container container) where TInterceptor : class
	//        {
	//            var interceptorRegistration = container.GetRegistration(typeof(TInterceptor));

	//            if (interceptorRegistration == null)
	//            {
	//                // This will throw an ActivationException
	//                container.GetInstance<TInterceptor>();
	//            }

	//            return interceptorRegistration.BuildExpression();
	//        }

	//        private static void RequiresIsNotNull(object instance, string paramName)
	//        {
	//            if (instance == null)
	//            {
	//                throw new ArgumentNullException(paramName);
	//            }
	//        }

	//        #endregion
	//    }
	//}
	// Extension methods for interceptor registration
	// NOTE: These extension methods can only intercept interfaces, not abstract types.
	public static class InterceptorExtensions
{
	public static void InterceptWith<TInterceptor>(this Container container,
		Func<Type, bool> predicate)
		where TInterceptor : class, IInterceptor
	{
		RequiresIsNotNull(container, "container");
		RequiresIsNotNull(predicate, "predicate");
		container.Options.ConstructorResolutionBehavior.GetConstructor(
			typeof(TInterceptor), typeof(TInterceptor));

		var interceptWith = new InterceptionHelper(container)
		{
			BuildInterceptorExpression =
				e => BuildInterceptorExpression<TInterceptor>(container),
			Predicate = type => predicate(type)
		};

		container.ExpressionBuilt += interceptWith.OnExpressionBuilt;
	}

	public static void InterceptWith(this Container container,
		Func<IInterceptor> interceptorCreator, Func<Type, bool> predicate)
	{
		RequiresIsNotNull(container, "container");
		RequiresIsNotNull(interceptorCreator, "interceptorCreator");
		RequiresIsNotNull(predicate, "predicate");

		var interceptWith = new InterceptionHelper(container)
		{
			BuildInterceptorExpression =
				e => Expression.Invoke(Expression.Constant(interceptorCreator)),
			Predicate = type => predicate(type)
		};

		container.ExpressionBuilt += interceptWith.OnExpressionBuilt;
	}

	public static void InterceptWith(this Container container,
		Func<ExpressionBuiltEventArgs, IInterceptor> interceptorCreator,
		Func<Type, bool> predicate)
	{
		RequiresIsNotNull(container, "container");
		RequiresIsNotNull(interceptorCreator, "interceptorCreator");
		RequiresIsNotNull(predicate, "predicate");

		var interceptWith = new InterceptionHelper(container)
		{
			BuildInterceptorExpression = e => Expression.Invoke(
				Expression.Constant(interceptorCreator),
				Expression.Constant(e)),
			Predicate = type => predicate(type)
		};

		container.ExpressionBuilt += interceptWith.OnExpressionBuilt;
	}

	public static void InterceptWith(this Container container,
		IInterceptor interceptor, Func<Type, bool> predicate)
	{
		RequiresIsNotNull(container, "container");
		RequiresIsNotNull(interceptor, "interceptor");
		RequiresIsNotNull(predicate, "predicate");

		var interceptWith = new InterceptionHelper(container)
		{
			BuildInterceptorExpression = e => Expression.Constant(interceptor),
			Predicate = predicate
		};

		container.ExpressionBuilt += interceptWith.OnExpressionBuilt;
	}

	[DebuggerStepThrough]
	private static Expression BuildInterceptorExpression<TInterceptor>(
		Container container)
		where TInterceptor : class
	{
		var interceptorRegistration = container.GetRegistration(typeof(TInterceptor));

		if (interceptorRegistration == null)
		{
			// This will throw an ActivationException
			container.GetInstance<TInterceptor>();
		}

		return interceptorRegistration.BuildExpression();
	}

	private static void RequiresIsNotNull(object instance, string paramName)
	{
		if (instance == null)
		{
			throw new ArgumentNullException(paramName);
		}
	}


}
}