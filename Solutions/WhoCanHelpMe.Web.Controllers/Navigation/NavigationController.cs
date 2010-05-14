using WhoCanHelpMe.Framework.Mapper;
using WhoCanHelpMe.Web.Controllers.Navigation.Mappers;

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
        private readonly ICmsTasks cmsTasks;
        private readonly ILinkViewModelMapper linkViewModelMapper;

        public NavigationController(IIdentityTasks identityTasks, ICmsTasks cmsTasks, ILinkViewModelMapper linkViewModelMapper)
        {
            this.identityTasks = identityTasks;
            this.cmsTasks = cmsTasks;
            this.linkViewModelMapper = linkViewModelMapper;
        }

        public ActionResult Menu()
        {
            return View(
                string.Empty, 
                string.Empty,
                new MenuViewModel
                {
                    IsLoggedIn = this.identityTasks.IsSignedIn(),
                    CmsLinks = cmsTasks.GetNavigationItems().MapAllUsing(linkViewModelMapper)
                });
        }
    }
}