namespace WhoCanHelpMe.Framework.Caching
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    #endregion

    /// <summary>
    /// </summary>
    public class Cacheable : ICacheable
    {
        /// <summary>
        /// This static member caches the domain signature properties to avoid looking them up for 
        /// each instance of the same type.
        /// A description of the very slick ThreadStatic attribute may be found at 
        /// http://www.dotnetjunkies.com/WebLog/chris.taylor/archive/2005/08/18/132026.aspx
        /// </summary>
        [ThreadStatic]
        private static Dictionary<Type, IEnumerable<PropertyInfo>> signaturePropertiesDictionary;

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        protected virtual IEnumerable<PropertyInfo> GetSignatureProperties()
        {
            IEnumerable<PropertyInfo> properties;

            // Init the signaturePropertiesDictionary here due to reasons described at 
            // http://blogs.msdn.com/jfoscoding/archive/2006/07/18/670497.aspx
            if (signaturePropertiesDictionary == null)
            {
                signaturePropertiesDictionary = new Dictionary<Type, IEnumerable<PropertyInfo>>();
            }

            if (signaturePropertiesDictionary.TryGetValue(this.GetType(), out properties))
            {
                return properties;
            }

            return signaturePropertiesDictionary[this.GetType()] = this.GetTypeSpecificSignatureProperties();
        }

        /// <summary>
        /// The getter for SignatureProperties for value objects should include the properties 
        /// which make up the entirety of the object's properties; that's part of the definition 
        /// of a value object.
        /// </summary>
        /// <remarks>
        /// This ensures that the value object has no properties decorated with the 
        /// [DomainSignature] attribute.
        /// </remarks>
        /// <returns>
        /// The get type specific signature properties.
        /// </returns>
        protected virtual IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties()
        {
            return this.GetType().GetProperties();
        }

        /// <summary>
        /// Gets the cache key for the object.
        /// </summary>
        /// <returns>
        /// A unique key that can be used to identify this instance of the object
        /// </returns>
        public string GenerateCacheKey()
        {
            return GenerateCacheKey(this);
        }

        /// <summary>
        /// Generate a cachekey from the ICacheable object.
        /// This method crawls the object and creates a string containing
        /// a key pair value for all properties and collections.
        /// </summary>
        /// <param name="cacheable">
        /// The cacheable.
        /// </param>
        /// <returns>
        /// String containing the flattened structure of the object with values.
        /// </returns>
        private static string GenerateCacheKey(Cacheable cacheable)
        {
            var sb = new StringBuilder();

            foreach (var property in cacheable.GetSignatureProperties())
            {
                var value = property.GetValue(cacheable, null);

                if (value is ICacheable)
                {
                    sb.Append((value as ICacheable).GenerateCacheKey());
                }
                else if (value is IEnumerable)
                {
                    sb.Append(property.Name);
                    sb.Append(":");

                    var collection = value as IEnumerable;

                    if (collection is IDictionary)
                    {
                        var dictionary = collection as IDictionary;

                        foreach (DictionaryEntry entry in dictionary)
                        {
                            sb.Append(entry.Key);
                            sb.Append(":");
                            sb.Append(entry.Value);
                        }
                    }
                    else if (collection is string)
                    {
                        sb.Append(collection);
                        sb.Append("/");
                    }
                    else
                    {
                        var enumerator = collection.GetEnumerator();

                        while (enumerator.MoveNext())
                        {
                            sb.Append(enumerator.Current);
                            sb.Append("|");
                        }

                        sb.Append("/");
                    }
                }
                else
                {
                    sb.Append(property.Name);
                    sb.Append(":");
                    sb.Append(value);
                    sb.Append("/");
                }
            }

            return sb.ToString();
        }
    }
}