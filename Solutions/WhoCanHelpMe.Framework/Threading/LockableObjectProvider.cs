namespace WhoCanHelpMe.Framework.Threading
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// For any given object provides a unique object that can be used with the Monitor class or with a lock statement.
    /// </summary>
    public static class LockableObjectProvider
    {
        private static readonly Dictionary<object, object> lockObjects = new Dictionary<object, object>(1000);

        /// <summary>
        /// Retrieves a lockable object for the specified input object
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object GetLockableObject(object o)
        {
            if (!lockObjects.ContainsKey(o))
            {
                lock (lockObjects)
                {
                    if (!lockObjects.ContainsKey(o))
                    {
                        lockObjects.Add(o, new object());
                    }
                }
            }

            return lockObjects[o];
        }
    }
}