namespace WhoCanHelpMe.Web.Initialisers
{
    using System;
    using System.Web.Mvc;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Spark;
    using Spark.Web.Mvc;

    public class ViewEngineInitialiser : IWindsorInstaller
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
                .AddNamespace("WhoCanHelpMe.Web.Code")
                .AddNamespace("xVal.Html");

            var services = SparkEngineStarter.CreateContainer(settings);

            SparkEngineStarter.RegisterViewEngine(services);
        }
    }
}