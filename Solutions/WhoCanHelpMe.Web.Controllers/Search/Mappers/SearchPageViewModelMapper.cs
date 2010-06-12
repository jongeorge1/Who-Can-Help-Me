namespace WhoCanHelpMe.Web.Controllers.Search.Mappers
{
    #region Using Directives

    using System.Collections.Generic;

    using Domain;

    using Framework.Mapper;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class SearchPageViewModelMapper : IMapper<IList<Tag>, SearchPageViewModel>
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        private readonly IMapper<Tag, TagViewModel> tagViewModelMapper;

        public SearchPageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IMapper<Tag, TagViewModel> tagViewModelMapper)
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