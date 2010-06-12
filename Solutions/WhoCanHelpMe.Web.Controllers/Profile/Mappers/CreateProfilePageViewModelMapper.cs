namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    #region Using Directives

    using Domain;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class CreateProfilePageViewModelMapper :
        BasePageViewModelMapper<CreateProfileDetails, CreateProfilePageViewModel>
    {
        public CreateProfilePageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
            : base(pageViewModelBuilder)
        {
        }
    }
}