namespace WhoCanHelpMe.Aspects.Caching
{
    #region Using Directives

    using System;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;

    using Framework.Caching;
    using Framework.Threading;

    using Microsoft.Practices.ServiceLocation;

    using PostSharp.Aspects;

    #endregion

    /// <summary>
    /// A PostSharp aspect that will cache the return value of a method or property. The aspect can 
    /// be applied with no paraemters, in which case a single return value is cached for the method, or
    /// with a varyByParameter method, which will result in a separate cached value for each input value of
    /// the named parameter.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CachedAttribute : MethodInterceptionAspect
    {
        /// <summary>
        /// Used as part of the null object pattern for testing
        /// </summary>
        private static readonly string NullObject = "~Zen.Core.Aspects.Caching.CachedAttribute.NullObject~";

        /// <summary>
        /// Holds an instance of the Caching Service.
        /// </summary>
        private static ICachingService cachingService;

        /// <summary>
        /// Backing field for the <see cref="Cache" /> field.
        /// </summary>
        private readonly CacheName cache;

        /// <summary>
        /// Holds the key used for adding and removing the item to the cache. If a parameter has been 
        /// specified, this is added to the key as required.
        /// </summary>
        private string cacheKey;

        /// <summary>
        /// Initializes a new instance of the CachedAttribute class.
        /// </summary>
        /// <param name="cache">
        /// The name of the cache to store the data
        /// </param>
        public CachedAttribute(CacheName cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// Gets an instance of the Caching Service
        /// </summary>
        public static ICachingService CachingService
        {
            get
            {
                cachingService = cachingService ?? ServiceLocator.Current.GetInstance<ICachingService>();
                return cachingService;
            }
        }

        /// <summary>
        /// Gets the cache in which the data should be stored.
        /// </summary>
        public CacheName Cache
        {
            get
            {
                return this.cache;
            }
        }

        /// <summary>
        /// Uses reflection to build a unique key to use for the cached values.
        /// The key takes the form:
        /// category::full-type-name.method-name:[vary-by-parameter-name=vary-by-parameter-value]
        /// </summary>
        /// <param name="method">
        /// Method to be executed.
        /// </param>
        /// <param name="aspectInfo">
        /// The aspect Info.
        /// </param>
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            AspectHelper.GetMethodSignature(method);

            // Use a StringBuilder to compile the data. Add the category, type and method names.
            var cacheKeyBuilder = new StringBuilder(method.DeclaringType.FullName);
            cacheKeyBuilder.Append(".");
            cacheKeyBuilder.Append(method.Name);

            // Store the compiled cache key in a local variable.
            this.cacheKey = cacheKeyBuilder.ToString();

            base.CompileTimeInitialize(method, aspectInfo);
        }

        /// <summary>
        /// Performs the caching / retrievel of the object.
        /// </summary>
        /// <param name="args">
        /// The event args.
        /// </param>
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            // Get the full cache key
            CacheKey fullCacheKey = this.GetCacheKey(args);

            // Check to see if the cache contains the target item
            args.ReturnValue = CachingService[fullCacheKey];

            if (args.ReturnValue == null)
            {
                lock (LockableObjectProvider.GetLockableObject(fullCacheKey))
                {
                    // Check again to see if the cache contains the target item (double check lock pattern)
                    args.ReturnValue = cachingService[fullCacheKey];

                    // If we still don't have a return value, proceed with the original method to populate it
                    if (args.ReturnValue == null)
                    {
                        args.Proceed();

                        CachingService.Add(fullCacheKey, args.ReturnValue ?? NullObject);
                    }
                }
            }

            if (args.ReturnValue is string && (string)args.ReturnValue == NullObject)
            {
                args.ReturnValue = null;
            }
        }

        /// <summary>
        /// Computes the cache key hash
        /// </summary>
        /// <param name="item">
        /// The item to cache
        /// </param>
        /// <returns>
        /// A MD5 Hash of the item.
        /// </returns>
        private string ComputeCacheKeyHash(string item)
        {
            string cacheKey = string.Empty;

            byte[] byteArray = Encoding.Unicode.GetBytes(item);

            using (var ms = new MemoryStream(byteArray))
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                byte[] hashedData = md5.ComputeHash(ms);

                ((IDisposable)md5).Dispose();

                cacheKey = Convert.ToBase64String(hashedData);
            }

            return cacheKey;
        }

        /// <summary>
        /// Returns the full cache key based on the previously built cache key and the current execution
        /// context.
        /// </summary>
        /// <param name="eventArgs">
        /// Arguments of the method that is being invoked. This will be used to gen the cache key
        /// </param>
        /// <returns>
        /// If the user has specified a parameter to vary by, the value of that parameter is appended
        /// to the previously built cache key and returned. Otherwise, just the previously built key is 
        /// returned.
        /// </returns>
        private CacheKey GetCacheKey(MethodInterceptionArgs eventArgs)
        {
            var fullCacheKey = new StringBuilder(this.cacheKey);

            object[] arguments = eventArgs.Arguments.ToArray();

            if (arguments != null)
            {
                foreach (object argument in arguments)
                {
                    fullCacheKey.Append("-");

                    if (argument is ICacheable)
                    {
                        var cacheable = argument as ICacheable;
                        fullCacheKey.Append(this.ComputeCacheKeyHash(cacheable.GenerateCacheKey()));
                    }
                    else
                    {
                        fullCacheKey.Append(this.ComputeCacheKeyHash(argument.ToString()));
                    }
                }
            }

            return new CacheKey(this.Cache, fullCacheKey.ToString());
        }
    }
}