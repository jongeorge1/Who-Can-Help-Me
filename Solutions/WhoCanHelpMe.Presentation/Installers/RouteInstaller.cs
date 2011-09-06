namespace WhoCanHelpMe.Presentation.Installers
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using WhoCanHelpMe.Presentation.Controllers;

    /// <summary>
    /// Responsible for all the MVC route registration
    /// </summary>
    public class RouteInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AreaRegistration.RegisterAllAreas();

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute(" { *favicon }", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            RouteTable.Routes.RouteExistingFiles = false;

            // ELMAH handles ELMAH
            RouteTable.Routes.IgnoreRoute("elmah.axd");

            // Add Default Route
            RouteTable.Routes.MapLowerCaseRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            // Uncomment to enable the route debugger, then browse to the URL you want to test as normal.
            // RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }
    }
}