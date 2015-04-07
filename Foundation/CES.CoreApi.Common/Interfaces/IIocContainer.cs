using System;
using CES.CoreApi.Common.Enumerations;

namespace CES.CoreApi.Common.Interfaces
{
    public interface IIocContainer
    {
        /// <summary>
        /// Solve TService dependency
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        /// <returns>instance of TService</returns>
        TService Resolve<TService>()
            where TService : class;

        /// <summary>
        /// Solves named TService dependency
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        /// <param name="name">Name of the instance</param>
        /// <returns>instance of TService</returns>
        TService Resolve<TService>(string name)
            where TService : class;

        /// <summary>
        /// Solves type dependency
        /// </summary>
        /// <returns>instance of object</returns>
        object Resolve(Type type);

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">arget type</typeparam>
        /// <param name="injectionMembers">Members injected</param>
        /// <returns></returns>
        IIocContainer RegisterType<TFrom, TTo>(params object[] injectionMembers)
            where TTo : TFrom;

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">arget type</typeparam>
        /// <param name="interceptionBehaviorType"></param>
        /// <param name="injectionMembers">Members injected</param>
        /// <param name="interceptorType"></param>
        /// <returns></returns>
        IIocContainer RegisterType<TFrom, TTo>(InterceptorType interceptorType,
            InterceptionBehaviorType interceptionBehaviorType, params object[] injectionMembers) 
            where TTo : TFrom;

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="lifetimeManagerType">Lifetime manager type</param>
        /// <returns></returns>
        IIocContainer RegisterType<TFrom, TTo>(LifetimeManagerType lifetimeManagerType) 
            where TTo : TFrom;

        /// <summary>
        /// Registers type on container
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="name">Name of the instance </param>
        /// <param name="injectionMembers">Members injected</param>
        /// <returns></returns>
        IIocContainer RegisterType<TFrom, TTo>(string name, params object[] injectionMembers) 
            where TTo : TFrom;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="name"></param>
        /// <param name="interceptorType"></param>
        /// <param name="interceptionBehaviorType"></param>
        /// <param name="injectionMembers"></param>
        /// <returns></returns>
        IIocContainer RegisterType<TFrom, TTo>(string name, InterceptorType interceptorType,
            InterceptionBehaviorType interceptionBehaviorType, params object[] injectionMembers) 
            where TTo : TFrom;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="lifetimeManagerType"></param>
        /// <param name="name"></param>
        /// <param name="interceptorType"></param>
        /// <param name="interceptionBehaviorType"></param>
        /// <param name="injectionMembers"></param>
        /// <returns></returns>
        IIocContainer RegisterType<TFrom, TTo>(LifetimeManagerType lifetimeManagerType,
            string name,
            InterceptorType interceptorType,
            InterceptionBehaviorType interceptionBehaviorType,
            params object[] injectionMembers)
            where TTo : TFrom;

        IIocContainer RegisterTypeWithInterfaceInterceptor<TFrom, TTo>(InterceptionBehaviorType interceptionBehaviorType,
            params object[] injectionMembers) where TTo : TFrom;

        /// <summary>
        /// Registers type on container using InterfaceInterceptor
        /// </summary>
        /// <typeparam name="TFrom">Source type</typeparam>
        /// <typeparam name="TTo">Target type</typeparam>
        /// <param name="name">Name of the instance </param>
        /// <param name="interceptionBehaviorType"></param>
        /// <param name="injectionMembers">Members injected</param>
        /// <returns></returns>
        IIocContainer RegisterTypeWithInterfaceInterceptor<TFrom, TTo>(string name, InterceptionBehaviorType interceptionBehaviorType, 
            params object[] injectionMembers) where TTo : TFrom;

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
        IIocContainer RegisterTypeWithInterfaceInterceptor<TFrom, TTo>(LifetimeManagerType lifetimeManagerType,
            string name, InterceptionBehaviorType interceptionBehaviorType, params object[] injectionMembers) where TTo : TFrom;

        IIocContainer RegisterTypeWithVirtualMethodInterceptor<TFrom, TTo>(InterceptionBehaviorType interceptionBehaviorType,
            params object[] injectionMembers) where TTo : TFrom;

        IIocContainer RegisterType<TFrom, TTo>(LifetimeManagerType lifetimeManagerType, params object[] injectionMembers) where TTo : TFrom;

        IIocContainer RegisterTypeWithInterfaceInterceptor<TFrom, TTo>(LifetimeManagerType lifetimeManagerType,
            InterceptionBehaviorType injectionMembers) where TTo : TFrom;
    }
}