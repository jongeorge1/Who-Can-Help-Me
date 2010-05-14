namespace WhoCanHelpMe.Framework.Caching
{
    /// <summary>
    /// Contract for a Key to store / retrieve items from a cache.
    /// </summary>
    public interface ICacheKey
    {
        /// <summary>
        /// Gets Key items in the cache can be stored / retrieved with.
        /// </summary>
        string Key
        {
            get;
        }

        /// <summary>
        /// Gets CultureName.
        /// </summary>
        string CultureName
        {
            get;
        }

        /// <summary>
        /// Gets CacheName.
        /// </summary>
        CacheName CacheName
        {
            get;
        }
    }
}