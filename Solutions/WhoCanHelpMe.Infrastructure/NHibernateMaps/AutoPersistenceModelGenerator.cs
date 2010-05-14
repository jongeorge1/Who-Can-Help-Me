namespace WhoCanHelpMe.Infrastructure.NHibernateMaps
{
    #region Using Directives

    using System;
    using System.Linq;

    using Conventions;

    using Domain;

    using FluentNHibernate;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Conventions;

    using SharpArch.Core.DomainModel;
    using SharpArch.Data.NHibernate.FluentNHibernate;

    #endregion

    /// <summary>
    /// Generates the automapping for the domain assembly
    /// </summary>
    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {
        public AutoPersistenceModel Generate()
        {
            var mappings = new AutoPersistenceModel();
            
            mappings.AddEntityAssembly(typeof(Profile).Assembly).Where(GetAutoMappingFilter);
            mappings.Conventions.Setup(GetConventions());
            mappings.Setup(GetSetup());
            mappings.IgnoreBase<Entity>();
            mappings.IgnoreBase(typeof(EntityWithTypedId<>));
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();

            return mappings;
        }

        /// <summary>
        /// Provides a filter for only including types which inherit from the IEntityWithTypedId interface.
        /// </summary>
        private bool GetAutoMappingFilter(Type t)
        {
            return t.GetInterfaces().Any(x =>
                                         x.IsGenericType && 
                                         x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>));
        }

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
                   {
                       c.Add<PrimaryKeyConvention>();
                       c.Add<CustomForeignKeyConvention>();
                       c.Add<HasManyConvention>();
                       c.Add<TableNameConvention>();
                   };
        }

        private Action<AutoMappingExpressions> GetSetup()
        {
            return c =>
                       {
                           c.FindIdentity = type => type.Name == "Id";
                       };
        }
    }
}