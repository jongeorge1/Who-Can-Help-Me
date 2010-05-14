namespace WhoCanHelpMe.Framework.Caching
{
    /// <summary>
    /// Contract for the behaviour of a Caching Service
    /// </summary>
    public interface ICachingService
    {
        /// <summary>
        /// Gets Count of items in the cache
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Retrieve an item from the cache with the specified Cache Key
        /// </summary>
        /// <param name="key">
        /// The Cache key.
        /// </param>
        object this[ICacheKey key]
        {
            get;
        }

        /// <summary>
        /// Add an item to the Cache
        /// </summary>
        /// <param name="key">
        /// The key used to store / retrieve item.
        /// </param>
        /// <param name="value">
        /// The value to store.
        /// </param>
        void Add(ICacheKey key, object value);

        /// <summary>
        /// Add an item to the Cache
        /// </summary>
        /// <param name="key">
        /// The key used to store / retrieve item.
        /// </param>
        /// <param name="value">
        /// The value to store.
        /// </param>
        /// <param name="cacheDuration">
        /// The cache duration.
        /// </param>
        void Add(ICacheKey key, object value, CacheDuration cacheDuration);

        /// <summary>
        /// Returns whether the cache contains the item
        /// </summary>
        /// <param name="key">
        /// The key of the item to check
        /// </param>
        /// <returns>
        /// Whether the cache contains the item with the key specified.
        /// </returns>
        bool Contains(ICacheKey key);

        /// <summary>
        /// Flush all items in the cache.
        /// </summary>
        void Flush();

        /// <summary>
        /// Flush all items in the named cache.
        /// </summary>
        /// <param name="cacheName">
        /// The cache name.
        /// </param>
        void Flush(CacheName cacheName);

        /// <summary>
        /// Remove an item with the specified key from the cache
        /// </summary>
        /// <param name="key">
        /// The key of the item to remove.
        /// </param>
        void Remove(ICacheKey key);
    }
}