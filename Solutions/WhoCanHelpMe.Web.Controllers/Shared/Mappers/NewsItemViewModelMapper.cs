namespace WhoCanHelpMe.Web.Controllers.Shared.Mappers
{
    #region Using Directives

    using Contracts;

    using Domain;

    using Framework.Mapper;

    using Home.ViewModels;

    #endregion

    public class NewsItemViewModelMapper : BaseMapper<NewsItem, NewsItemViewModel>,
                                           INewsItemViewModelMapper
    {
    }
}