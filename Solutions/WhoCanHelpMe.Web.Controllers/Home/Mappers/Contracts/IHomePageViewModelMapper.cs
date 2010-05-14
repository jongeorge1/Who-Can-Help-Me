namespace WhoCanHelpMe.Web.Controllers.Home.Mappers.Contracts
{
    #region Using Directives

    using System.Collections.Generic;

    using Domain;
    using Domain.Cms.Pages;
    using Framework.Mapper;

    using ViewModels;

    #endregion

    public interface IHomePageViewModelMapper : IMapper<IList<NewsItem>, HomePage, HomePageViewModel>
    {
    }
}