namespace WhoCanHelpMe.Infrastructure.Caching
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Caching;

    using Framework.Caching;

    #endregion

    public class HttpCachingService : ICachingService
    {
        private readonly Cache cache = new Cache();
        private readonly Dictionary<CacheDuration, int> cacheDurations;
        private readonly Dictionary<CacheName, CacheDuration> caches;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="enabled">True to turn standard caching on, false to disable it.</param>
        /// <param name="outputCachingDurationSeconds">Duration in seconds of the output cache. 0 means that output caching is disabled.</param>
        /// <param name="cacheDurations"></param>
        /// <param name="caches">A dictionary in which the keys are the names of the caches and the values indicate whether those caches are enabled or not.</param>
        public HttpCachingService(
            bool enabled,
            Dictionary<CacheDuration, int> cacheDurations,
            Dictionary<CacheName, CacheDuration> caches)
        {
            this.cacheDurations = cacheDurations;
            this.caches = caches;

            if (enabled && HttpContext.Current == null)
            {
                throw new ArgumentException("Caching cannot be enabled when there is no current HttpContext.", "enabled");
            }

            this.cache = HttpContext.Current.Cache;

            this.Enabled = enabled;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public int Count
        {
            get
            {
                if (this.Enabled)
                {
                    return this.cache.Count;
                }

                return 0;
            }
        }

        public object this[ICacheKey key]
        {
            get
            {
                if (this.Enabled)
                {
                    if (this.Contains(key))
                    {
                        return this.cache[(CacheKey)key];
                    }
                    
                    return null;
                }

                return null;
            }
        }

        public void Add(ICacheKey key, object value)
        {
            if (this.ShouldCache(key))
            {
                if (this.caches.ContainsKey(key.CacheName))
                {
                    this.Add(key, value, this.caches[key.CacheName]);
                }
                else
                {
                    this.Add(key, value, CacheDuration.Permanent);
                }
            }
        }

        public void Add(ICacheKey key, object value, CacheDuration cacheDuration)
        {
            if (this.ShouldCache(key))
            {
                this.cache.Add(
                    (CacheKey)key,
                    value,
                    null,
                    DateTime.Now.AddSeconds(this.DurationInSeconds(cacheDuration)),
                    TimeSpan.Zero,
                    CacheItemPriority.Normal,
                    null);
            }
        }

        public bool Contains(ICacheKey key)
        {
            if (this.Enabled)
            {
                return (this.cache[(CacheKey)key] != null);
            }

            return false;
        }

        public void Flush()
        {
            if (this.Enabled)
            {
                IDictionaryEnumerator enumerator = this.cache.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    CacheKey enumeratorKey = (CacheKey)enumerator.Key.ToString();

                    if (enumeratorKey != null)
                    {
                        this.cache.Remove(enumeratorKey);
                    }
                }
            }
        }

        public void Flush(CacheName cache)
        {
            if (this.Enabled)
            {
                IDictionaryEnumerator enumerator = this.cache.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    CacheKey enumeratorKey = (CacheKey) enumerator.Key.ToString();

                    if (enumeratorKey != null && enumeratorKey.CacheName == cache)
                    {
                        this.cache.Remove(enumeratorKey);
                    }
                }
            }
        }

        public void Remove(ICacheKey key)
        {
            if (this.Enabled)
            {
                this.cache.Remove((CacheKey)key);
            }
        }

        private bool ShouldCache(ICacheKey key)
        {
            if (this.caches.ContainsKey(key.CacheName))
            {
                return this.Enabled && this.caches[key.CacheName] != CacheDuration.NotCached;
            }

            return false;
        }

        private int DurationInSeconds(CacheDuration duration)
        {
            return this.cacheDurations[duration];
        }
    }
}