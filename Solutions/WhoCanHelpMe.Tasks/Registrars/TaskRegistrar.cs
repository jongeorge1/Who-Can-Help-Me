namespace WhoCanHelpMe.Tasks.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    #endregion

    public class TasksRegistrar : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssembly(Assembly.GetAssembly(typeof(TasksRegistrar))).Pick().WithService.DefaultInterface());
        }
    }
}