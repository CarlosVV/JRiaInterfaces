using System;
using System.Globalization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace CES.CoreApi.UnityProxy
{
    public class UnityProxy : IIocContainer
    {
        #region Core

        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes UnityProxy instance
        /// </summary>
        /// <param name="container">Unity IoC container</param>
        public UnityProxy(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            _container = container;
        }

        #endregion //Core

        #region Public methods

        /// <summary>
        /// Solves TService dependency
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        /// <returns>instance of TService</returns>
        public TService Resolve<TService>() where TService : class
        {
            return _container.Resolve<TService>();
        }

        /// <summary>
        /// Solves named TService dependency
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        /// <param name="name">Name of the instance</param>
        /// <returns>instance of TService</returns>
        public TService Resolve<TService>(string name) where TService : class
        {
            return _container.Resolve<TService>(name);
        }

        /// <summary>
        /// Solves type dependency
        /// </summary>
        /// <returns>instance of object</returns>
        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="injectionMembers">Members injected</param>
        /// <returns></returns>
        public IIocContainer RegisterType<TFrom, TTo>(params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(LifetimeManagerType.Container, string.Empty, InterceptorType.Undefined, InterceptionBehaviorType.Undefined, injectionMembers);
            return this;
        }

        public IIocContainer RegisterType<TFrom, TTo>(LifetimeManagerType lifetimeManagerType, params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(lifetimeManagerType, string.Empty, InterceptorType.Undefined, InterceptionBehaviorType.Undefined, injectionMembers);
            return this;
        }

        public IIocContainer RegisterType<TFrom, TTo>(InterceptorType interceptorType, InterceptionBehaviorType interceptionBehaviorType,
            params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(LifetimeManagerType.Container, string.Empty, interceptorType, interceptionBehaviorType, injectionMembers);
            return this;
        }

        public IIocContainer RegisterTypeWithInterfaceInterceptor<TFrom, TTo>(InterceptionBehaviorType interceptionBehaviorType,
            params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(LifetimeManagerType.Container, string.Empty, InterceptorType.InterfaceInterceptor, interceptionBehaviorType, injectionMembers);
            return this;
        }

        public IIocContainer RegisterTypeWithVirtualMethodInterceptor<TFrom, TTo>(InterceptionBehaviorType interceptionBehaviorType,
            params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(LifetimeManagerType.Container, string.Empty, InterceptorType.VirtualMethodInterceptor, interceptionBehaviorType, injectionMembers);
            return this;
        }

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="lifetimeManagerType">Lifetime manager type</param>
        /// <returns></returns>
        public IIocContainer RegisterType<TFrom, TTo>(LifetimeManagerType lifetimeManagerType) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(lifetimeManagerType, string.Empty, InterceptorType.Undefined, InterceptionBehaviorType.Undefined);
            return this;
        }

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="name">Name of the instance </param>
        /// <param name="injectionMembers">Members injected</param>
        /// <returns></returns>
        public IIocContainer RegisterType<TFrom, TTo>(string name, params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(LifetimeManagerType.Container, name, InterceptorType.Undefined, InterceptionBehaviorType.Undefined, injectionMembers);
            return this;
        }

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="name">Name of the instance </param>
        /// <param name="interceptionBehaviorType"></param>
        /// <param name="injectionMembers">Members injected</param>
        /// <param name="interceptorType"></param>
        /// <returns></returns>
        public IIocContainer RegisterType<TFrom, TTo>(string name, InterceptorType interceptorType,
            InterceptionBehaviorType interceptionBehaviorType, params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(LifetimeManagerType.Container, name, interceptorType, interceptionBehaviorType, injectionMembers);
            return this;
        }

        /// <summary>
        /// Registers type on container using InterfaceInterceptor
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="name">Name of the instance </param>
        /// <param name="interceptionBehaviorType"></param>
        /// <param name="injectionMembers">Members injected</param>
        /// <returns></returns>
        public IIocContainer RegisterTypeWithInterfaceInterceptor<TFrom, TTo>(string name, InterceptionBehaviorType interceptionBehaviorType,
            params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(LifetimeManagerType.Container, name, InterceptorType.InterfaceInterceptor, interceptionBehaviorType, injectionMembers);
            return this;
        }

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="lifetimeManagerType">Lifetime manager type</param>
        /// <param name="name">Name of the instance </param>
        /// <param name="interceptionBehaviorType"></param>
        /// <param name="injectionMembers">Members injected</param>
        /// <returns></returns>
        public IIocContainer RegisterTypeWithInterfaceInterceptor<TFrom, TTo>(LifetimeManagerType lifetimeManagerType,
            string name, InterceptionBehaviorType interceptionBehaviorType, params object[] injectionMembers) where TTo : TFrom
        {
            RegisterType<TFrom, TTo>(lifetimeManagerType, name, InterceptorType.InterfaceInterceptor,
                interceptionBehaviorType, injectionMembers);
            return this;
        }

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="lifetimeManagerType">Lifetime manager type</param>
        /// <param name="name">Name of the instance </param>
        /// <param name="interceptorType"></param>
        /// <param name="interceptionBehaviorType"></param>
        /// <param name="injectionMembers">Members injected</param>
        /// <returns></returns>
        public IIocContainer RegisterType<TFrom, TTo>(LifetimeManagerType lifetimeManagerType,
                                                      string name,
                                                      InterceptorType interceptorType,
                                                      InterceptionBehaviorType interceptionBehaviorType,
                                                      params object[] injectionMembers) where TTo : TFrom
        {
            //Get lifetime manager
            var lifetimeManager = GetLifetimeManager(lifetimeManagerType);

            //Get injection members
            var injections = GetInjectionMembers(interceptorType, interceptionBehaviorType, injectionMembers);

            //Register type
            RegisterType<TFrom, TTo>(name, lifetimeManager, injections);

            return this;
        }

        #endregion //Public methods

        #region Private methods

        private void RegisterType<TFrom, TTo>(string name, LifetimeManager lifetimeManager, InjectionMember[] injections)
            where TTo : TFrom
        {
            if (!string.IsNullOrEmpty(name) && injections == null)
                _container.RegisterType<TFrom, TTo>(name, lifetimeManager);
            if (!string.IsNullOrEmpty(name) && injections != null)
                _container.RegisterType<TFrom, TTo>(name, lifetimeManager, injections);
            if (string.IsNullOrEmpty(name) && injections == null)
                _container.RegisterType<TFrom, TTo>(lifetimeManager);
            if (string.IsNullOrEmpty(name) && injections != null)
                _container.RegisterType<TFrom, TTo>(lifetimeManager, injections);
        }

        private static InjectionMember[] GetInjectionMembers(InterceptorType interceptorType,
            InterceptionBehaviorType interceptionBehaviorType, object[] injectionMembers)
        {
            var isInterceptionRequired = interceptorType != InterceptorType.Undefined &&
                                         interceptionBehaviorType != InterceptionBehaviorType.Undefined;

            var isContstructorParmetersDefined = injectionMembers.Length != 0;

            //Get list of injected members
            InjectionMember[] injections = null;

            if (isInterceptionRequired && !isContstructorParmetersDefined)
            {
                injections = new InjectionMember[]
                {
                    GetBehavior(interceptionBehaviorType),
                    GetInterceptor(interceptorType)
                };
            }

            if (isInterceptionRequired && isContstructorParmetersDefined)
            {
                injections = new InjectionMember[]
                {
                    GetBehavior(interceptionBehaviorType),
                    GetInterceptor(interceptorType),
                    new InjectionConstructor(injectionMembers)
                };
            }

            if (!isInterceptionRequired && isContstructorParmetersDefined)
            {
                injections = new InjectionMember[]
                {
                    new InjectionConstructor(injectionMembers)
                };
            }
            return injections;
        }

        /// <summary>
        /// Creates lifetime manager instance according type
        /// </summary>
        /// <param name="lifetimeManagerType">Lifetime manager type</param>
        /// <returns></returns>
        private static LifetimeManager GetLifetimeManager(LifetimeManagerType lifetimeManagerType)
        {
            switch (lifetimeManagerType)
            {
                case LifetimeManagerType.Undefined: //Default lifetime manager
                    return new ContainerControlledLifetimeManager();

                case LifetimeManagerType.Container:
                    return new ContainerControlledLifetimeManager();

                case LifetimeManagerType.AlwaysNew:
                    return new TransientLifetimeManager();

                default:
                    throw new ApplicationException(string.Format(CultureInfo.InvariantCulture,
                        "Unsupported lifetime manager type detected {0}",
                        lifetimeManagerType));

            }
        }

        private static Interceptor GetInterceptor(InterceptorType interceptorType)
        {
            switch (interceptorType)
            {
                case InterceptorType.VirtualMethodInterceptor:
                    return new Interceptor(new VirtualMethodInterceptor());

                case InterceptorType.InterfaceInterceptor:
                    return new Interceptor(new InterfaceInterceptor());

                case InterceptorType.TransparentProxyInterceptor:
                    return new Interceptor(new TransparentProxyInterceptor());

                default:
                    throw new ApplicationException(string.Format(CultureInfo.InvariantCulture,
                        "Unsupported interceptor type detected {0}",
                        interceptorType));
            }
        }

        private static InterceptionBehavior GetBehavior(InterceptionBehaviorType behaviorType)
        {
            switch (behaviorType)
            {
                case InterceptionBehaviorType.Performance:
                    return new InterceptionBehavior<UnityPerformanceInterceptorBehavior>();

                default:
                    return null;
            }
        }

        #endregion //Private methods
    }
}
