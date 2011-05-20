namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers.Contracts
{
    using WhoCanHelpMe.Web.Controllers.Profile.ViewModels;

    public interface ICreateProfilePageViewModelBuilder
    {
        CreateProfilePageViewModel Get();
    }
}