namespace WhoCanHelpMe.Web.Controllers.Registrars
{
    using System.ComponentModel.Composition;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Castle.Windsor;
    using Domain.Contracts.Container;
    using N2.Castle;
    using N2.Web.Mvc;

    /// <summary>
    /// Registers the ASP.NET MVC Routes with the CMS system so that 
    /// CMS pages can be resolved as URLs.
    /// </summary>
    [Export(typeof(IComponentRegistrar))]
    public class N2RouteRegistrar : IComponentRegistrar
    {
        /// <summary>
        /// Initialises the N2 engine and registers all the routes
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public void Register(IWindsorContainer container)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute(" { *favicon }", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            RouteTable.Routes.RouteExistingFiles = false;

            // Initialise N2 for MVC
            container.Kernel.RemoveComponent("CachingService");
            var engine = MvcEngine.Create(new WindsorServiceContainer(container));

            if (engine != null)
            {
                // ************************************************************************
                // This route detects N2 content item paths and executes their controller
                // All pages should be in the CMS system so we shouldn't need to add
                // any more custom routes!
                // ************************************************************************
                RouteTable.Routes.Add(new ContentRoute(engine));
            }

            // Add the default route
            RouteTable.Routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = string.Empty });
        }
    }
}