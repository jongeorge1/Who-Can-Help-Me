namespace WhoCanHelpMe.Framework.Caching
{
    /// <summary>
    /// Defines different Durations that items should be cached for
    /// </summary>
    public enum CacheDuration
    {
        /// <summary>
        /// item should not cached.
        /// </summary>
        NotCached,

        /// <summary>
        /// item should be cached for a very short time.
        /// </summary>
        VeryShort,

        /// <summary>
        /// item should be cached for a short time.
        /// </summary>
        Short,

        /// <summary>
        /// item should be cached for a medium amount of time.
        /// </summary>
        Medium,

        /// <summary>
        /// item should be cahced for a long time.
        /// </summary>
        Long,

        /// <summary>
        /// item should be permanently cached.
        /// </summary>
        Permanent
    }
}