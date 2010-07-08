namespace WhoCanHelpMe.Framework.Container
{
    using System.Reflection;
    using System.Web;

    using Framework.Contracts.Container;

    using System.ComponentModel.Composition.Hosting;

    using MEF;

    using WhoCanHelpMe.Framework.Extensions;

    public static class ComponentInitialiser
    {
        public static void Initialise()
        {
            var catalog = new CatalogBuilder()
                              .ForAssembly(typeof(IComponentRegistrarMarker).Assembly)
                              .ForMvcAssembly(Assembly.GetExecutingAssembly())
                              .ForMvcAssembliesInDirectory(HttpRuntime.BinDirectory, "WhoCanHelpMe*.dll") // Won't work in Partial trust
                              .Build();

            var compositionContainer = new CompositionContainer(catalog);

            compositionContainer
                .GetExports<IComponentInitialiser>()
                .Each(e => e.Value.Initialise());
        }
    }
}