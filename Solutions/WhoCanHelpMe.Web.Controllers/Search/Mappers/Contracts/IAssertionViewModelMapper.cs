namespace WhoCanHelpMe.Web.Controllers.Search.Mappers.Contracts
{
    #region Using Directives

    using Domain;

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public interface IAssertionViewModelMapper : IMapper<Assertion, AssertionViewModel>
    {
    }
}