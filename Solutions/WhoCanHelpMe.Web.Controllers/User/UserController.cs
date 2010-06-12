namespace WhoCanHelpMe.Web.Controllers.User
{
    #region Using Directives

    using System.Security.Authentication;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using Home;

    using MvcContrib;
    using MvcContrib.Filters;

    using WhoCanHelpMe.Framework.Mapper;
    using WhoCanHelpMe.Web.Controllers.User.ViewModels;

    #endregion

    public class UserController : BaseController
    {
        private readonly IIdentityTasks identityTasks;

        private readonly IMapper<string, string, LoginPageViewModel> loginPageViewModelMapper;

        public UserController(
            IIdentityTasks identityTasks,
            IMapper<string, string, LoginPageViewModel> loginPageViewModelMapper)
        {
            this.identityTasks = identityTasks;
            this.loginPageViewModelMapper = loginPageViewModelMapper;
        }

        [ValidateInput(false)]
        [ModelStateToTempData]
        public ActionResult Authenticate(
            string openId,
            string returnUrl)
        {
            try
            {
                this.identityTasks.Authenticate(openId);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }

                return this.RedirectToAction<HomeController>(x => x.Index());
            }
            catch (AuthenticationException ex)
            {
                this.TempData.Add(
                    "Message",
                    ex.Message);
                return this.RedirectToAction(x => x.Login(string.Empty));
            }
        }

        public ActionResult Index()
        {
            if (this.identityTasks.IsSignedIn())
            {
                return this.RedirectToAction<HomeController>(x => x.Index());
            }

            return this.RedirectToAction(x => x.Login(string.Empty));
        }

        [ModelStateToTempData]
        public ActionResult Login(string returnUrl)
        {
            if (this.identityTasks.IsSignedIn())
            {
                //  Redirect to home page
                return this.RedirectToAction<HomeController>(x => x.Index());
            }

            var message = this.TempData["Message"] as string;

            var viewModel = this.loginPageViewModelMapper.MapFrom(
                message,
                returnUrl);

            return this.View(viewModel);
        }

        public ActionResult SignOut()
        {
            this.identityTasks.SignOut();

            return this.RedirectToAction<HomeController>(x => x.Index());
        }
    }
}