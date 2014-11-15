using CES.CoreApi.Common.Enumerations;

namespace CES.CoreApi.Common.Interfaces
{
    public interface IIocContainerFactory
    {
        IIocContainer GetInstance(InterceptionBehaviorType interceptionBehaviorTypes);
    }
}