namespace WhoCanHelpMe.Web.Controllers.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Framework.Contracts.Container;

    using Properties;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class MapperRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(
                   AllTypes.FromAssembly(Assembly.GetAssembly(typeof(ControllersRegistrarMarker)))
                           .Pick()
                           .If(f => f.Namespace.Contains(".Mappers"))
                           .WithService.DefaultInterface());
        }
    }
}