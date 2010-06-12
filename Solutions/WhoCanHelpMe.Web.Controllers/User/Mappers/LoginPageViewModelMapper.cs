namespace WhoCanHelpMe.Web.Controllers.User.Mappers
{
    #region Using Directives

    using WhoCanHelpMe.Framework.Mapper;
    using WhoCanHelpMe.Web.Controllers.Shared.Mappers.Contracts;
    using WhoCanHelpMe.Web.Controllers.User.ViewModels;

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