namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    #region Using Directives

    using Contracts;

    using Domain;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class CreateProfilePageViewModelMapper :
        BasePageViewModelMapper<CreateProfileDetails, CreateProfilePageViewModel>,
        ICreateProfilePageViewModelMapper
    {
        public CreateProfilePageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
            : base(pageViewModelBuilder)
        {
        }
    }
}