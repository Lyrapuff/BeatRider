using System.Collections.Generic;
using General.Behaviours;

namespace General.Storage.Cache
{
    public class SessionCache : SingletonBehaviour<SessionCache>
    {
        private Dictionary<string, object> _cache = new Dictionary<string, object>();

        public void Set(string key, object obj)
        {
            _cache[key] = obj;
        }

        public T Get<T>(string key) where T : class
        {
            if (_cache.ContainsKey(key))
            {
                return _cache[key] as T;
            }

            return null;
        }
    }
}