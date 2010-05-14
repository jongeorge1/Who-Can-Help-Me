namespace WhoCanHelpMe.Web.Controllers.Extensions
{
    #region Using Directives

    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    #endregion

    public static class RouteCollectionExtensions
    {
        public static void MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapRouteLowercase(name, url, defaults, null);
        }

        public static void MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults,
                                             object constraints)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");

            if (url == null)
                throw new ArgumentNullException("url");

            var route = new LowercaseRoute(url, new MvcRouteHandler())
                            {
                                Defaults = new RouteValueDictionary(defaults),
                                Constraints = new RouteValueDictionary(constraints)
                            };

            if (String.IsNullOrEmpty(name))
                routes.Add(route);
            else
                routes.Add(name, route);
        }
    }
}