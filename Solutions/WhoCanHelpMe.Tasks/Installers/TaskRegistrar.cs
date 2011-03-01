namespace WhoCanHelpMe.Tasks.Installers
{
    #region Using Directives

    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    #endregion

    public class TasksInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssembly(Assembly.GetAssembly(typeof(TasksInstaller))).Pick().WithService.DefaultInterface());
        }
    }
}