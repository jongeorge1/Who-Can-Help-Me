namespace WhoCanHelpMe.Infrastructure.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Framework.Contracts.Container;

    using Framework.Extensions;

    using Properties;

    using SharpArch.Core.PersistenceSupport;
    using SharpArch.Core.PersistenceSupport.NHibernate;
    using SharpArch.Data.NHibernate;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class GenericRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.AddComponent(
                    "entityDuplicateChecker",
                    typeof(IEntityDuplicateChecker),
                    typeof(EntityDuplicateChecker));

            container.AddComponent(
                    "repositoryType",
                    typeof(IRepository<>),
                    typeof(Repository<>));

            container.AddComponent(
                    "nhibernateRepositoryType",
                    typeof(INHibernateRepository<>),
                    typeof(NHibernateRepository<>));

            container.AddComponent(
                    "repositoryWithTypedId",
                    typeof(IRepositoryWithTypedId<,>),
                    typeof(RepositoryWithTypedId<,>));

            container.AddComponent(
                    "nhibernateRepositoryWithTypedId",
                    typeof(INHibernateRepositoryWithTypedId<,>),
                    typeof(NHibernateRepositoryWithTypedId<,>));
        }
    }
}