namespace WhoCanHelpMe.Web.Controllers.About.Mappers
{
    #region Using Directives

    using System.Collections.Generic;

    using AutoMapper;

    using Domain;

    using Framework.Mapper;

    using Shared.Mappers.Contracts;

    using ViewModels;

    using WhoCanHelpMe.Web.Controllers.Home.ViewModels;

    #endregion

    public class AboutPageViewModelMapper : BasePageViewModelMapper<IList<NewsItem>, AboutPageViewModel>
    {
        private readonly IMapper<NewsItem, NewsItemViewModel> newsItemViewModelMapper;

        public AboutPageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IMapper<NewsItem, NewsItemViewModel> newsItemViewModelMapper)
            : base(pageViewModelBuilder)
        {
            this.newsItemViewModelMapper = newsItemViewModelMapper;
        }

        protected override void CreateMap()
        {
            Mapper.CreateMap<IList<NewsItem>, AboutPageViewModel>().ConvertUsing(list => this.DoMapping(list));
        }

        private AboutPageViewModel DoMapping(IList<NewsItem> input)
        {
            return new AboutPageViewModel
                {
                    NewsItems = input.MapAllUsing(this.newsItemViewModelMapper)
                };
        }
    }
}