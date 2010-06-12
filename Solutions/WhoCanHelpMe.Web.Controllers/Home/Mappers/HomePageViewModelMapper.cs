namespace WhoCanHelpMe.Web.Controllers.Home.Mappers
{
    #region Using Directives

    using System.Collections.Generic;

    using AutoMapper;

    using Domain;

    using Framework.Mapper;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class HomePageViewModelMapper : BasePageViewModelMapper<IList<NewsItem>, HomePageViewModel>,
                                           IMapper<IList<NewsItem>, HomePageViewModel>
    {
        private readonly IMapper<NewsItem, NewsItemViewModel> newsItemViewModelMapper;

        public HomePageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IMapper<NewsItem, NewsItemViewModel> newsItemViewModelMapper)
            : base(pageViewModelBuilder)
        {
            this.newsItemViewModelMapper = newsItemViewModelMapper;
        }

        protected override void CreateMap()
        {
            Mapper.CreateMap<IList<NewsItem>, HomePageViewModel>().ConvertUsing(list => this.DoMapping(list));
        }

        private HomePageViewModel DoMapping(IList<NewsItem> input)
        {
            return new HomePageViewModel
                {
                    NewsItems = input.MapAllUsing(this.newsItemViewModelMapper)
                };
        }
    }
}