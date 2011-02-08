namespace WhoCanHelpMe.Web.Controllers.Registrars
{
    #region Using Directives

    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    #endregion

    public class MapperRegistrar : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                                       .Where(t => t.Namespace.EndsWith("Mappers"))
                                       .WithService.FirstInterface());
        }
    }
}