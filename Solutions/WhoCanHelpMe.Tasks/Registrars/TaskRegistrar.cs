namespace WhoCanHelpMe.Tasks.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using WhoCanHelpMe.Framework.Contracts.Container;
    using WhoCanHelpMe.Tasks.Properties;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class TasksRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(AllTypes.FromAssembly(Assembly.GetAssembly(typeof(TasksRegistrarMarker))).Pick().WithService.DefaultInterface());
        }
    }
}