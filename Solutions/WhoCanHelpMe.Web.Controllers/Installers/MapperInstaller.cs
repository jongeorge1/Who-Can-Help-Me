namespace WhoCanHelpMe.Web.Controllers.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class MapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                                       .Where(t => t.Namespace.EndsWith("Mappers"))
                                       .WithService.FirstInterface());
        }
    }
}