namespace WhoCanHelpMe.Web.Controllers.Search.Mappers.Contracts
{
    #region Using Directives

    using System.Collections.Generic;

    using Domain;

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public interface ISearchResultsPageViewModelMapper : IMapper<IList<Assertion>, IList<Tag>, SearchResultsPageViewModel>
    {
    }
}