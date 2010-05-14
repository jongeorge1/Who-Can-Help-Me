namespace WhoCanHelpMe.Web.Controllers.Search.Mappers
{
    #region Using Directives

    using Contracts;

    using Domain;

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public class AssertionViewModelMapper : BaseMapper<Assertion, AssertionViewModel>,
                                            IAssertionViewModelMapper
    {
    }
}