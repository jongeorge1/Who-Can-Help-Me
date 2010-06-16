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
    using WhoCanHelpMe.Framework.Security;
    using WhoCanHelpMe.Web.Controllers.User.ViewModels;

    #endregion

    public class UserController : BaseController
    {
        private readonly IIdentityService identityService;

        private readonly IMapper<string, string, LoginPageViewModel> loginPageViewModelMapper;

        public UserController(
            IIdentityService identityService,
            IMapper<string, string, LoginPageViewModel> loginPageViewModelMapper)
        {
            this.identityService = identityService;
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
                this.identityService.Authenticate(openId);

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
            if (this.identityService.IsSignedIn())
            {
                return this.RedirectToAction<HomeController>(x => x.Index());
            }

            return this.RedirectToAction(x => x.Login(string.Empty));
        }

        [ModelStateToTempData]
        public ActionResult Login(string returnUrl)
        {
            if (this.identityService.IsSignedIn())
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
            this.identityService.SignOut();

            return this.RedirectToAction<HomeController>(x => x.Index());
        }
    }
}