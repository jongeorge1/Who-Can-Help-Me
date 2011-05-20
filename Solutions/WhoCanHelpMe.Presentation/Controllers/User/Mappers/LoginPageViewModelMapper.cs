namespace WhoCanHelpMe.Presentation.Controllers.User.Mappers
{
    #region Using Directives

    using SharpArch.Futures.Core.Mapping;

    using WhoCanHelpMe.Presentation.Controllers.Shared.Mappers.Contracts;
    using WhoCanHelpMe.Presentation.Controllers.User.ViewModels;

    #endregion

    public class LoginPageViewModelMapper : IMapper<string, string, LoginPageViewModel>
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        public LoginPageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public LoginPageViewModel MapFrom(string message, string returnUrl)
        {
            var viewModel = new LoginPageViewModel { Message = message ?? string.Empty, ReturnUrl = returnUrl };

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }
    }
}