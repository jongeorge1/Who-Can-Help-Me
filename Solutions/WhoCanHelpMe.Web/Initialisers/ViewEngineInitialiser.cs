namespace WhoCanHelpMe.Web.Initialisers
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Web.Mvc;

    using Framework.Contracts.Container;
    
    using Spark;
    using Spark.Web.Mvc;

    #endregion

    [Export(typeof(IComponentInitialiser))]
    public class ViewEngineInitialiser : IComponentInitialiser
    {
        public void Initialise()
        {
            ViewEngines.Engines.Clear();

            var settings = new SparkSettings()
                .SetAutomaticEncoding(true)
                .SetPageBaseType("WhoCanHelpMe.Web.Views.SparkModelViewPage")
                .AddNamespace("Microsoft.Web.Mvc")
                .AddNamespace("MvcContrib.FluentHtml")
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