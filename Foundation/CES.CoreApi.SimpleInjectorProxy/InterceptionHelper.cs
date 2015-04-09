using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleInjector;

namespace CES.CoreApi.SimpleInjectorProxy
{
    internal class InterceptionHelper
    {
        private static readonly MethodInfo NonGenericInterceptorCreateProxyMethod = (
            from method in typeof (Interceptor).GetMethods()
            where method.Name == "CreateProxy"
            where method.GetParameters().Length == 3
            select method)
            .Single();

        public InterceptionHelper(Container container)
        {
            Container = container;
        }

        internal Container Container { get; private set; }

        internal Func<ExpressionBuiltEventArgs, Expression> BuildInterceptorExpression
        {
            get;
            set;
        }

        internal Func<Type, bool> Predicate { get; set; }

        [DebuggerStepThrough]
        public void OnExpressionBuilt(object sender, ExpressionBuiltEventArgs e)
        {
            if (Predicate(e.RegisteredServiceType))
            {
                ThrowIfServiceTypeNotAnInterface(e);
                e.Expression = BuildProxyExpression(e);
            }
        }

        [DebuggerStepThrough]
        private static void ThrowIfServiceTypeNotAnInterface(ExpressionBuiltEventArgs e)
        {
            // NOTE: We can only handle interfaces, because
            // System.Runtime.Remoting.Proxies.RealProxy only supports interfaces.
            if (!e.RegisteredServiceType.IsInterface)
            {
                throw new NotSupportedException("Can't intercept type " +
                    e.RegisteredServiceType.Name + " because it is not an interface.");
            }
        }

        [DebuggerStepThrough]
        private Expression BuildProxyExpression(ExpressionBuiltEventArgs e)
        {
            var interceptor = BuildInterceptorExpression(e);

            // Create call to
            // (ServiceType)Interceptor.CreateProxy(Type, IInterceptor, object)
            var proxyExpression =
                Expression.Convert(
                    Expression.Call(NonGenericInterceptorCreateProxyMethod,
                        Expression.Constant(e.RegisteredServiceType, typeof(Type)),
                        interceptor,
                        e.Expression),
                    e.RegisteredServiceType);

            if (e.Expression is ConstantExpression && interceptor is ConstantExpression)
            {
                return Expression.Constant(CreateInstance(proxyExpression),
                    e.RegisteredServiceType);
            }

            return proxyExpression;
        }

        [DebuggerStepThrough]
        private static object CreateInstance(Expression expression)
        {
            var instanceCreator = Expression.Lambda<Func<object>>(expression,
                new ParameterExpression[0])
                .Compile();

            return instanceCreator();
        }
    }
}
