namespace WhoCanHelpMe.Web.Controllers.Search.Mappers
{
    #region Using Directives

    using System.Collections.Generic;

    using Domain;

    using Framework.Mapper;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class SearchResultsPageViewModelMapper : IMapper<IList<Assertion>, IList<Tag>, SearchResultsPageViewModel>
    {
        private readonly IMapper<Assertion, AssertionViewModel> assertionViewModelMapper;

        private readonly IPageViewModelBuilder pageViewModelBuilder;

        private readonly IMapper<Tag, TagViewModel> tagViewModelMapper;

        public SearchResultsPageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IMapper<Assertion, AssertionViewModel> assertionViewModelMapper,
            IMapper<Tag, TagViewModel> tagViewModelMapper)
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