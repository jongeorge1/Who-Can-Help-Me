namespace WhoCanHelpMe.Web.Code
{
    #region Using Directives

    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    #endregion

    /// <summary>
    /// Contains extension methods for the HttpRequest object.
    /// </summary>
    public static class HttpRequestExtensions
    {
        static List<string> fileExtensionsToIgnore = new List<string>
            { 
                ".css", 
                ".js",
                ".jpg",
                ".gif",
                ".png",
                ".html",
                ".htm",
                ".pdf",
                ".ico",
                ".web",
            };

        /// <summary>
        /// Examines the request to see if the current file
        /// is a static file (i.e. .htm, .css, .js)
        /// </summary>
        /// <param name="request">
        /// The current HttpRequest
        /// </param>
        /// <returns>Whether the current request is for a static file</returns>
        public static bool IsStaticFile(this HttpRequest request)
        {
            bool isStaticFile = false;

            try
            {
                var file = new FileInfo(request.Url.Segments[request.Url.Segments.Length - 1]);

                string extension = file.Extension.ToLowerInvariant();

                isStaticFile = fileExtensionsToIgnore.Contains(extension);
            }
            catch
            {
                // TODO: Log
            }

            return isStaticFile;
        }

        /// <summary>
        /// Examines the request to see if the current file
        /// needs to be authenticated
        /// </summary>
        /// <remarks>
        /// Because IIS7 runs in integrated mode, non aspx files
        /// are handled by the ASP.NET runtime. This function allows
        /// us to specify a list of file types that are safe to ignore.
        /// This function can be used to prevent expensive authentication
        /// logic from being executed for static files.
        /// </remarks>
        /// <param name="request">
        /// The current HttpRequest
        /// </param>
        /// <returns>
        /// Whether the current request needs to be authenticated.
        /// </returns>
        public static bool RequiresAuthentication(this HttpRequest request)
        {
            return !request.IsStaticFile();
        }
    }
}