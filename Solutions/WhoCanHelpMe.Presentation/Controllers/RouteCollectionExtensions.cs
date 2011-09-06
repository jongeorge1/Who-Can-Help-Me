namespace WhoCanHelpMe.Presentation.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteCollectionExtensions
    {
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url)
        {
            return MapLowerCaseRoute(routes, name, url, null /* defaults */, (object)null /* constraints */);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return MapLowerCaseRoute(routes, name, url, defaults, (object)null /* constraints */);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            return MapLowerCaseRoute(routes, name, url, defaults, constraints, null /* namespaces */);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return MapLowerCaseRoute(routes, name, url, null /* defaults */, null /* constraints */, namespaces);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            return MapLowerCaseRoute(routes, name, url, defaults, null /* constraints */, namespaces);
        }

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is not a regular URL as it may contain special routing characters.")]
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new LowercaseRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }
    }
}