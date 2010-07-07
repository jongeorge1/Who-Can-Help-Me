namespace WhoCanHelpMe.Web.Controllers.Initialisers
{
    #region Using Directives
    
    using System.ComponentModel.Composition;
    using System.Web.Mvc;
    using System.Web.Routing;

    using WhoCanHelpMe.Framework.Contracts.Container;

    // use for route debugging
    using MvcContrib.Routing;
    using Extensions;

    #endregion

    /// <summary>
    /// Responsible for all the MVC route registration
    /// </summary>
    [Export(typeof(IComponentInitialiser))]
    public class RouteInitialiser : IComponentInitialiser
    {
        /// <summary>
        /// Registers the routes into the routes collection
        /// </summary>
        public void Initialise()
        {
            AreaRegistration.RegisterAllAreas();

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute(" { *favicon }", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            RouteTable.Routes.RouteExistingFiles = false;

            // ELMAH handles ELMAH
            RouteTable.Routes.IgnoreRoute("elmah.axd");

            // Add Default Route
            RouteTable.Routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            // Uncomment to enable the route debugger, then browse to the URL you want to test as normal.
            // RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }
    }
}