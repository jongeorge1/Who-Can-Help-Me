namespace WhoCanHelpMe.Web.Controllers.Search.Mappers
{
    #region Using Directives

    using System.Collections.Generic;

    using Contracts;

    using Domain;

    using Framework.Mapper;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class SearchResultsPageViewModelMapper : ISearchResultsPageViewModelMapper
    {
        private readonly IAssertionViewModelMapper assertionViewModelMapper;

        private readonly IPageViewModelBuilder pageViewModelBuilder;

        private readonly ITagViewModelMapper tagViewModelMapper;

        public SearchResultsPageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IAssertionViewModelMapper assertionViewModelMapper,
            ITagViewModelMapper tagViewModelMapper)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
            this.assertionViewModelMapper = assertionViewModelMapper;
            this.tagViewModelMapper = tagViewModelMapper;
        }

        public SearchResultsPageViewModel MapFrom(
            IList<Assertion> assertions,
            IList<Tag> tags)
        {
            var result = new SearchResultsPageViewModel
                {
                    MatchingAssertions = assertions.MapAllUsing(this.assertionViewModelMapper),
                    PopularTags = tags.MapAllUsing(this.tagViewModelMapper)
                };

            return this.pageViewModelBuilder.UpdateSiteProperties(result);
        }
    }
}