namespace Ottoman.MemoryCache
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public partial class MemoryCacheManager : ICacheManager
    {
        private static MemoryCacheManager _cacheManager;

        private MemoryCacheManager()
        {
        }

        public static MemoryCacheManager Instance
        {
            get
            {
                if (_cacheManager == null)
                {
                    _cacheManager = new MemoryCacheManager();
                }

                return _cacheManager;
            }
        }


        /// <summary>
        /// Cache object
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            return (T)this.Cache[key];
        }

        public virtual object Get(string key)
        {
            return this.Cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set(string key, object data, int? cacheTime = 0)
        {
            if (data == null)
            {
                return;
            }

            CacheItemPolicy cachePolicy = new CacheItemPolicy();

            if (cacheTime.HasValue && cacheTime > 0)
            {
                cachePolicy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime.GetValueOrDefault());
            }
            else
            {
                cachePolicy.AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration;
            }

            this.Cache.Add(new CacheItem(key, data), cachePolicy);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key)
        {
            return (this.Cache.Contains(key));
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            this.Cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            foreach (var item in this.Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (string key in keysToRemove)
            {
                this.Remove(key);
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in this.Cache)
                this.Remove(item.Key);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}