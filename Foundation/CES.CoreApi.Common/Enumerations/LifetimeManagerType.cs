namespace CES.CoreApi.Common.Enumerations
{
    /// <summary>
    /// Types of object lifetime management used by IoC contianers
    /// </summary>
    public enum LifetimeManagerType
    {
        Undefined = 0,
        Container = 1,
        AlwaysNew = 2
    }
}