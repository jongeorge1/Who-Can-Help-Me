namespace WhoCanHelpMe.Framework.Caching
{
    #region Using Directives

    using System;
    using System.Globalization;

    #endregion

    /// <summary>
    /// Defines the CacheKey to be used to store and retrieve items from the Caching Service
    /// </summary>
    public class CacheKey : ICacheKey
    {
        /// <summary>
        /// internal key.
        /// </summary>
        private readonly string internalKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKey"/> class.
        /// </summary>
        /// <param name="cacheName">
        /// The cache name.
        /// </param>
        /// <param name="key">
        /// The cache key.
        /// </param>
        public CacheKey(CacheName cacheName, string key)
            : this(cacheName, key, CultureInfo.CurrentCulture.Name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKey"/> class.
        /// </summary>
        /// <param name="cacheName">
        /// The cache name.
        /// </param>
        /// <param name="key">
        /// The cache key.
        /// </param>
        /// <param name="cultureName">
        /// The culture name.
        /// </param>
        internal CacheKey(CacheName cacheName, string key, string cultureName)
        {
            this.CacheName = cacheName;
            this.Key = key;
            this.CultureName = cultureName;

            this.internalKey = string.Format(
                CultureInfo.InvariantCulture,
                "Zen::{0}:{1}:{2}",
                this.CultureName,
                this.CacheName,
                this.Key);
        }

        /// <summary>
        /// Gets CacheName.
        /// </summary>
        public CacheName CacheName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets CultureName.
        /// </summary>
        public string CultureName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets Key of the item being cached.
        /// </summary>
        public string Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="System.String"/> to <see cref="CacheKey"/>.
        /// </summary>
        /// <param name="passedKey">The passed key.</param>
        /// <returns>The result of the conversion or null if the conversion failed.</returns>
        public static explicit operator CacheKey(string passedKey)
        {
            if (string.IsNullOrEmpty(passedKey))
            {
                return null;
            }

            string[] data = passedKey.Split(new string[] { "::" }, StringSplitOptions.None);

            if (data.Length != 2 || data[0] != "Zen")
            {
                return null;
            }

            string[] subData = data[1].Split(':');

            if (subData.Length != 3)
            {
                return null;
            }

            try
            {
                string cultureName = subData[0];
                var cacheName = (CacheName)Enum.Parse(typeof(CacheName), subData[1]);
                string key = subData[2];

                return new CacheKey(cacheName, key, cultureName);
            }
            catch (ArgumentException)
            {   
                // Handle the various exceptions that could result from the Parse and conversion methods
                // Also covers ArgumentNullException
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
            catch (OverflowException)
            {
                return null;
            }
        }

        /// <summary>
        /// op_ implicit.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator string(CacheKey target)
        {
            return target.ToString();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.
        /// </exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            var target = obj as CacheKey;

            if (target == null)
            {
                return false;
            }

            return this.internalKey.Equals(target.internalKey);
        }

        /// <summary>
        /// get hash code.
        /// </summary>
        /// <returns>
        /// The get hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return this.internalKey.GetHashCode();
        }

        /// <summary>
        /// to string.
        /// </summary>
        /// <returns>
        /// The to string.
        /// </returns>
        public override string ToString()
        {
            return this.internalKey;
        }
    }
}