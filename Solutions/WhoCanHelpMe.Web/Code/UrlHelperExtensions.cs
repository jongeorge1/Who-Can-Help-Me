namespace WhoCanHelpMe.Web.Code
{
    #region Using Directives
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using WhoCanHelpMe.Framework.Extensions;

    using Cadenza.Web.ResourceVersioning;

    #endregion

    /// <summary>
    /// UrlHelper extension methods. 
    /// </summary>
    /// <remarks>
    /// All of which taken from sample solr mvc application. NK.
    /// </remarks>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Helper method to return a formatted stylesheet url
        /// </summary>
        /// <param name="urlHelper">
        /// The url helper.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The formatted url.
        /// </returns>
        public static string Stylesheet(this UrlHelper urlHelper, string fileName)
        {
            string versionedFileName = ResourceVersioningProviderManager.DefaultProvider.AddVersionNumberToFileName(fileName);

            return urlHelper.Content("~/Views/_Content/css/{0}").FormatWith(versionedFileName);
        }

        /// <summary>
        /// Helper method to return a formatted javascript url
        /// </summary>
        /// <param name="urlHelper">
        /// The url helper.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// Fully qualified url.
        /// </returns>
        public static string Scripts(this UrlHelper urlHelper, string fileName)
        {
            string versionedFileName = ResourceVersioningProviderManager.DefaultProvider.AddVersionNumberToFileName(fileName);

            return urlHelper.Content("~/Scripts/{0}").FormatWith(versionedFileName);
        }

        /// <summary>
        /// Imageses the specified URL helper.
        /// </summary>
        /// <param name="urlHelper">
        /// The URL helper.
        /// </param>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        /// <returns>
        /// Fully qualified url.
        /// </returns>
        public static string Images(this UrlHelper urlHelper, string fileName)
        {
            return urlHelper.Content("~/Views/_Content/img/{0}").FormatWith(fileName);
        }

        /// <summary>
        /// Sets/changes an url's query string parameter.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="url">URL to process</param>
        /// <param name="key">Query string parameter key to set/change</param>
        /// <param name="value">Query string parameter value</param>
        /// <returns>Resulting URL</returns>
        public static string SetParameter(this UrlHelper helper, string url, string key, string value)
        {
            return helper.SetParameters(url, new Dictionary<string, object> { { key, value } });
        }

        /// <summary>
        /// Sets/changes an url's query string parameters.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="url">URL to process</param>
        /// <param name="parameters">Paramteres to set/change</param>
        /// <returns>Resulting URL</returns>
        public static string SetParameters(this UrlHelper helper, string url, IDictionary<string, object> parameters)
        {
            var parts = url.Split('?');
            IDictionary<string, string> qs = new Dictionary<string, string>();
            if (parts.Length > 1)
            {
                qs = ParseQueryString(parts[1]);
            }

            foreach (var p in parameters)
            {
                qs[p.Key] = ToNullOrString(p.Value);
            }

            return parts[0] + "?" + DictToQuerystring(qs);
        }

        /// <summary>
        /// Removes parameters from an url's query string
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="url">URL to process</param>
        /// <param name="parameters">Query string parameter keys to remove</param>
        /// <returns>Resulting URL</returns>
        public static string RemoveParametersUrl(this UrlHelper helper, string url, params string[] parameters)
        {
            var parts = url.Split('?');
            IDictionary<string, string> qs = new Dictionary<string, string>();
            if (parts.Length > 1)
            {
                qs = ParseQueryString(parts[1]);
            }

            foreach (var p in parameters)
            {
                qs.Remove(p);
            }

            return parts[0] + "?" + DictToQuerystring(qs);
        }

        /// <summary>
        /// Removes the parameters.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>URL without specified parameters.</returns>
        public static string RemoveParameters(this UrlHelper helper, params string[] parameters)
        {
            return helper.RemoveParametersUrl(helper.RequestContext.HttpContext.Request.RawUrl, parameters);
        }

        /// <summary>
        /// Dicts to querystring.
        /// </summary>
        /// <param name="qs">The querystring dictionary.</param>
        /// <returns>Converts dictionary to string.</returns>
        public static string DictToQuerystring(IDictionary<string, string> qs)
        {
            var array = qs.Where(k => !string.IsNullOrEmpty(k.Key))
                .Select(k => string.Format("{0}={1}", HttpUtility.UrlEncode((string)k.Key), HttpUtility.UrlEncode((string)k.Value))).ToArray();
            return string.Join("&", array);
        }

        /// <summary>
        /// Sets/changes a single parameter from the current query string.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="key">Parameter key</param>
        /// <param name="value">Parameter value</param>
        /// <returns>Resulting URL</returns>
        public static string SetParameter(this UrlHelper helper, string key, object value)
        {
            return helper.SetParameter(helper.RequestContext.HttpContext.Request.RawUrl, key, ToNullOrString(value));
        }

        /// <summary>
        /// Sets/changes the current query string's parameters, using <paramref name="parameterDictionary"/> as dictionary
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="parameterDictionary">Parameters to set/change</param>
        /// <returns>Resulting URL</returns>
        public static string SetParameters(this UrlHelper helper, object parameterDictionary)
        {
            return helper.SetParameters(helper.RequestContext.HttpContext.Request.RawUrl, ToPropertyDictionary(parameterDictionary));
        }

        /// <summary>
        /// Parses a query string. If duplicates are present, the last key/value is kept.
        /// </summary>
        /// <param name="s">The querystring to parse.</param>
        /// <returns>Dictionary of querystring.</returns>
        public static IDictionary<string, string> ParseQueryString(string s)
        {
            var d = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            if (s == null)
            {
                return d;
            }

            if (s.StartsWith("?"))
            {
                s = s.Substring(1);
            }

            foreach (var kv in s.Split('&'))
            {
                var v = kv.Split('=');
                if (string.IsNullOrEmpty(v[0]))
                {
                    continue;
                }

                d[HttpUtility.UrlDecode(v[0])] = HttpUtility.UrlDecode(v[1]);
            }

            return d;
        }

        /// <summary>
        /// Toes the null or string.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns>Null or the ToString of the object.</returns>
        public static string ToNullOrString(object o)
        {
            return o == null ? null : o.ToString();
        }

        /// <summary>
        /// Builds a dictionary from the object's properties
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns>Dictionary of object.</returns>
        public static IDictionary<string, object> ToPropertyDictionary(object o)
        {
            if (o == null)
            {
                return null;
            }

            return o.GetType().GetProperties()
                .Select(p => new KeyValuePair<string, object>(p.Name, p.GetValue(o, null)))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}