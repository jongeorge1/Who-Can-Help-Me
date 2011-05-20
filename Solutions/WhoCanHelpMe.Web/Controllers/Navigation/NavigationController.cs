namespace WhoCanHelpMe.Web.Controllers.Navigation
{
    #region Using Directives

    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using ViewModels;

    using WhoCanHelpMe.Framework.Security;

    #endregion

    public class NavigationController : BaseController
    {
        private readonly IIdentityService identityService;

        public NavigationController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public ActionResult Menu()
        {
            var viewModel = new MenuViewModel
                {
                    IsLoggedIn = this.identityService.IsSignedIn()
                };

            return this.View(
                string.Empty,
                string.Empty,
                viewModel);
        }
    }
}