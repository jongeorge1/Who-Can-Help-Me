namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    #region Using Directives

    using Contracts;

    using Domain;

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public class ProfileAssertionViewModelMapper : BaseMapper<Assertion, ProfileAssertionViewModel>,
                                                   IProfileAssertionViewModelMapper
    {
    }
}