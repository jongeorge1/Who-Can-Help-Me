namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers.Contracts
{
    #region Using Directives

    using Domain;

    using ViewModels;

    #endregion

    public interface IAddAssertionDetailsMapper
    {
        AddAssertionDetails MapFrom(
            AddAssertionFormViewModel viewModel,
            Identity identity);
    }
}