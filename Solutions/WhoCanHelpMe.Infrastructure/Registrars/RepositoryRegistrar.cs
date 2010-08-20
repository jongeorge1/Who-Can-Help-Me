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

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class RepositoryRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(
                    AllTypes.Pick()
                            .FromAssembly(Assembly.GetAssembly(typeof(InfrastructureRegistrarMarker)))
                            .If(f => f.Namespace.Equals("WhoCanHelpMe.Infrastructure.Repositories"))
                            .WithService.FirstNonGenericInterface("WhoCanHelpMe.Domain.Contracts.Repositories"));
        }
    }
}