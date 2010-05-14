namespace WhoCanHelpMe.Framework.Container.MEF
{
    #region Using Directives
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.ComponentModel.Composition.ReflectionModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Web.Mvc;

    #endregion

    public class MvcCatalog : ComposablePartCatalog, ICompositionElement
    {
        #region Fields

        private readonly object locker = new object();

        private IQueryable<ComposablePartDefinition> parts;

        private readonly Type[] types;

        #endregion

        public MvcCatalog(params Type[] types)
        {
            this.types = types;
        }

        public MvcCatalog(Assembly assembly)
        {
            try
            {
                this.types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException typeLoadException)
            {
                this.types = typeLoadException.Types;
            }
        }

        public MvcCatalog(string dir, string pattern)
        {
            var fileSet = new List<Type>();

            foreach (var fileName in Directory.GetFiles(dir, pattern))
            {
                var assembly = Assembly.LoadFrom(fileName);
                fileSet.AddRange(assembly.GetExportedTypes());
            }

            this.types = fileSet.ToArray();
        }

        IQueryable<ComposablePartDefinition> InternalParts
        {
            get
            {
                if (this.parts == null)
                {
                    lock (this.locker)
                    {
                        if (this.parts == null)
                        {
                            var partsCollection = new List<ComposablePartDefinition>();

                            foreach (var type in this.types)
                            {
                                var typeCatalog = new TypeCatalog(type);
                                var part = typeCatalog.Parts.FirstOrDefault();

                                if (part == null) continue;

                                if (typeof(IController).IsAssignableFrom(type))
                                {
                                    part = new ControllerPartDefinitionDecorator(part, type, type.Name, type.Namespace);
                                }

                                partsCollection.Add(part);
                            }

                            Thread.MemoryBarrier();

                            this.parts = partsCollection.AsQueryable();
                        }
                    }
                }

                return this.parts;
            }
        }

        string ICompositionElement.DisplayName
        {
            get { return "MvcCatalog"; }
        }

        ICompositionElement ICompositionElement.Origin
        {
            get { return null; }
        }

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get { return InternalParts; }
        }

        class ControllerPartDefinitionDecorator : ComposablePartDefinition
        {
            #region Fields

            private readonly Type controllerType;
            private readonly string controllerName;
            private readonly string controllerNamespace;

            private readonly ComposablePartDefinition inner;

            private readonly object locker = new object();
            private IEnumerable<ExportDefinition> redefinedExports;

            #endregion

            public ControllerPartDefinitionDecorator(
                ComposablePartDefinition inner,
                Type controllerType,
                string controllerName,
                string controllerNamespace)
            {
                this.inner = inner;
                this.controllerType = controllerType;
                this.controllerName = controllerName;
                this.controllerNamespace = controllerNamespace;
            }

            public override IEnumerable<ExportDefinition> ExportDefinitions
            {
                get
                {
                    if (this.redefinedExports == null)
                    {
                        lock (this.locker)
                        {
                            if (this.redefinedExports == null)
                            {
                                var exports = this.inner.ExportDefinitions;
                                var metadata = new Dictionary<string, object>();
                                metadata[MefConstants.ControllerNameMetadataName] = this.controllerName.Substring(0, this.controllerName.Length - "Controller".Length);
                                metadata[MefConstants.ControllerNamespaceMetadataName] = this.controllerNamespace;
                                metadata[MefConstants.ExportedTypeIdentityMetadataName] = MefConstants.ControllerTypeIdentity;

                                var controllerExport = ReflectionModelServices.CreateExportDefinition(
                                    new LazyMemberInfo(this.controllerType),
                                    MefConstants.ControllerContract,
                                    new Lazy<IDictionary<string, object>>(() => metadata),
                                    this.inner as ICompositionElement);

                                Thread.MemoryBarrier();

                                this.redefinedExports = exports.Union(new[] { controllerExport }).ToArray();
                            }
                        }
                    }

                    return this.redefinedExports;
                }
            }

            public override IEnumerable<ImportDefinition> ImportDefinitions
            {
                get { return this.inner.ImportDefinitions; }
            }

            public override ComposablePart CreatePart()
            {
                return this.inner.CreatePart();
            }
        }
    }
}