namespace WhoCanHelpMe.Presentation.Controllers.Profile.Mappers.Contracts
{
    using WhoCanHelpMe.Presentation.Controllers.Profile.ViewModels;

    public interface ICreateProfilePageViewModelBuilder
    {
        CreateProfilePageViewModel Get();
    }
}