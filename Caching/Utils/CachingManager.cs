using Business_Logic_Layer.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using System.Collections;
using MemoryCache = System.Runtime.Caching.MemoryCache;

namespace Caching.Utils
{
    public class CachingManager
    {
        public delegate object GetCollection();
        public object GetCollectionFromCache(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(key);
        }

        public bool AddCollectionToCache(string key, object value, DateTimeOffset absExpiration)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, value, absExpiration);
        }

        public void DeleteCollectionFromCache(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }
    }
}
