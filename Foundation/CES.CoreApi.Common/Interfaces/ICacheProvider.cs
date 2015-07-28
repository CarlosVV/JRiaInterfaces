using System;

namespace CES.CoreApi.Common.Interfaces
{
    public interface ICacheProvider
    {
        void AddItem(string key, object value);
        void AddItem(string key, object value, TimeSpan timeout);
        T GetItem<T>(string key, Func<T> getDataFunc);
        T GetItem<T>(string key, Func<T> getDataFunc, TimeSpan timeout, Func<T, bool> isCacheValid = null);
        void RemoveItem(string key);
        void ClearCache();
    }
}
