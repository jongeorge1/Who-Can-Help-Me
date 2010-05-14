namespace WhoCanHelpMe.Web.Controllers.User.ViewModels
{
    #region Using Directives

    using Shared.ViewModels;

    #endregion

    public class LoginPageViewModel : PageViewModel
    {
        public string Message { get; set; }

        public string ReturnUrl { get; set; }
    }
}