namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    #region Using Directives

    using Shared.Mappers.Contracts;

    using ViewModels;

    using WhoCanHelpMe.Web.Controllers.Profile.Mappers.Contracts;

    #endregion

    public class CreateProfilePageViewModelBuilder : ICreateProfilePageViewModelBuilder
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        public CreateProfilePageViewModelBuilder(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public CreateProfilePageViewModel Get()
        {
            return this.pageViewModelBuilder.UpdateSiteProperties(new CreateProfilePageViewModel());
        }
    }
}