namespace WhoCanHelpMe.Web.Controllers.Extensions
{
    #region Using Directives

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Extension methods for TempDataDictionary
    /// </summary>
    public static class TempDataExtensions
    {
        /// <summary>
        /// Adds an object to the dictionary using the type name as the key
        /// </summary>
        /// <param name="tempData">
        /// The temp data.
        /// </param>
        /// <param name="obj">
        /// The obj to add.
        /// </param>
        public static void Add(this TempDataDictionary tempData, object obj)
        {
            tempData.Add(obj.GetType().Name, obj);
        }

        /// <summary>
        /// Gets an object from the dictionary using the type name as the key
        /// </summary>
        /// <param name="tempData">
        /// The temp data.
        /// </param>
        /// <typeparam name="T">
        /// The object type
        /// </typeparam>
        /// <returns>
        /// The object
        /// </returns>
        public static T Get<T>(this TempDataDictionary tempData)
        {
            object obj;

            if (tempData.TryGetValue(typeof(T).Name, out obj))
            {
                return (T) obj;
            }

            return default(T);
        }
    }
}