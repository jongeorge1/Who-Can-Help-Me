namespace WhoCanHelpMe.Web.Controllers.Navigation
{
    #region Using Directives

    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using ViewModels;

    #endregion

    public class NavigationController : BaseController
    {
        private readonly IIdentityTasks identityTasks;

        public NavigationController(IIdentityTasks identityTasks)
        {
            this.identityTasks = identityTasks;
        }

        public ActionResult Menu()
        {
            var viewModel = new MenuViewModel
                {
                    IsLoggedIn = this.identityTasks.IsSignedIn()
                };

            return this.View(
                string.Empty,
                string.Empty,
                viewModel);
        }
    }
}