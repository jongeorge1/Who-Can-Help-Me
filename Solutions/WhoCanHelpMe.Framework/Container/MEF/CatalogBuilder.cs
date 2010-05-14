namespace WhoCanHelpMe.Framework.Container.MEF
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Reflection;

    #endregion

    public class CatalogBuilder
    {
        private readonly IList<ComposablePartCatalog> catalogs = new List<ComposablePartCatalog>();

        public CatalogBuilder ForAssembly(Assembly assembly)
        {
            this.catalogs.Add(new AssemblyCatalog(assembly));

            return this;
        }

        public CatalogBuilder ForMvcAssembly(Assembly assembly)
        {
            this.catalogs.Add(new MvcCatalog(assembly));

            return this;
        }

        public CatalogBuilder ForMvcAssembliesInDirectory(string directory, string pattern)
        {
            this.catalogs.Add(new MvcCatalog(directory, pattern));

            return this;
        }

        public ComposablePartCatalog Build()
        {
            return new AggregateCatalog(this.catalogs);
        }
    }
}