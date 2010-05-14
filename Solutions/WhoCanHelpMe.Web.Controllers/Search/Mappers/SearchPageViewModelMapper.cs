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

    public class SearchPageViewModelMapper : ISearchPageViewModelMapper
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        private readonly ITagViewModelMapper tagViewModelMapper;

        public SearchPageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            ITagViewModelMapper tagViewModelMapper)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
            this.tagViewModelMapper = tagViewModelMapper;
        }

        public SearchPageViewModel MapFrom(IList<Tag> input)
        {
            var viewModel = new SearchPageViewModel
                {
                    PopularTags = input.MapAllUsing(this.tagViewModelMapper)
                };

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }
    }
}