namespace WhoCanHelpMe.Web.Installers
{
    using System.Linq;
    using System.Web.Mvc;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Spark;
    using Spark.Web.Mvc;

    using WhoCanHelpMe.Web.Controllers.Home;

    public class ViewEngineInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ViewEngines.Engines.Clear();

            var settings = new SparkSettings()
                .SetAutomaticEncoding(true)
                .AddNamespace("Microsoft.Web.Mvc")
                .AddNamespace("System")
                .AddNamespace("System.Collections.Generic")
                .AddNamespace("System.Linq")
                .AddNamespace("System.Web.Mvc")
                .AddNamespace("System.Web.Mvc.Html")
                .AddNamespace("WhoCanHelpMe.Framework.Extensions")
                .AddNamespace("WhoCanHelpMe.Web.Code");

            // Add all namespaces from controllers project.
            typeof(HomeController).Assembly
                                  .GetExportedTypes()
                                  .Select(t => t.Namespace)
                                  .Distinct()
                                  .ToList()
                                  .ForEach(n => settings.AddNamespace(n));

            var services = SparkEngineStarter.CreateContainer(settings);

            SparkEngineStarter.RegisterViewEngine(services);
        }
    }
}