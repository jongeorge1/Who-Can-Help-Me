namespace WhoCanHelpMe.Web.Code
{
    #region Using Directives
    
    using System.Web;
    
    #endregion

    /// <summary>
    /// Resources helper methods
    /// </summary>
    public static class Resources
    {
        /// <summary>
        /// Gets a global resource in the Site set for the specified key
        /// </summary>
        /// <param name="resourceKey">
        /// The resource key.
        /// </param>
        /// <returns>
        /// The resource string
        /// </returns>
        public static string Site(string resourceKey)
        {
            return GetGlobalResource(ResourceSets.Site, resourceKey);
        }

        /// <summary>
        /// Gets a global resource for the specified resource set and key
        /// </summary>
        /// <param name="set">
        /// The resource set.
        /// </param>
        /// <param name="resourceKey">
        /// The resource key.
        /// </param>
        /// <returns>
        /// The resource string
        /// </returns>
        private static string GetGlobalResource(ResourceSets set, string resourceKey)
        {
            var resourceSetName = string.Format("WhoCanHelpMe.Web.ResourceFiles.{0}", set);
            return HttpContext.GetGlobalResourceObject(resourceSetName, resourceKey) as string;
        }
    }
}