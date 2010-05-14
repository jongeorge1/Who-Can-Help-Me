namespace WhoCanHelpMe.Web.Controllers.Home.Mappers
{
    #region Using Directives

    using System.Collections.Generic;

    using AutoMapper;

    using Contracts;

    using Domain;

    using Framework.Mapper;

    using Shared.Mappers.Contracts;

    using ViewModels;

    #endregion

    public class HomePageViewModelMapper : BasePageViewModelMapper<IList<NewsItem>, HomePageViewModel>,
                                           IHomePageViewModelMapper
    {
        private readonly INewsItemViewModelMapper newsItemViewModelMapper;

        public HomePageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            INewsItemViewModelMapper newsItemViewModelMapper)
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