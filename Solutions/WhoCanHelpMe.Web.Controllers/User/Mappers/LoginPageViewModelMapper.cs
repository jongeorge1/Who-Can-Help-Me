namespace WhoCanHelpMe.Web.Controllers.User.Mappers
{
    #region Using Directives

    using Contracts;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class LoginPageViewModelMapper : ILoginPageViewModelMapper
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        public LoginPageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public LoginPageViewModel MapFrom(
            string message,
            string returnUrl)
        {
            var viewModel = new LoginPageViewModel
                {
                    Message = message ?? string.Empty,
                    ReturnUrl = returnUrl
                };

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }
    }
}