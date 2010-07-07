namespace WhoCanHelpMe.Framework.Container
{
    #region Using Directives

    using System.Reflection;
    using System.Security.Permissions;
    using System.Web;

    using Castle.Windsor;

    using Framework.Contracts.Container;

    using System.ComponentModel.Composition.Hosting;

    using Enumerable;

    using MEF;

    #endregion

    public static class ComponentRegistrar
    {
        public static void Register(IWindsorContainer container)
        {
            var catalog = new CatalogBuilder()
                              .ForAssembly(typeof(IComponentRegistrarMarker).Assembly)
                              .ForMvcAssembly(Assembly.GetExecutingAssembly())
                              .ForMvcAssembliesInDirectory(HttpRuntime.BinDirectory, "WhoCanHelpMe*.dll") // Won't work in Partial trust
                              .Build();

            var compositionContainer = new CompositionContainer(catalog);

            compositionContainer
                .GetExports<IComponentRegistrar>()
                .Each(e => e.Value.Register(container));
        }
    }
}