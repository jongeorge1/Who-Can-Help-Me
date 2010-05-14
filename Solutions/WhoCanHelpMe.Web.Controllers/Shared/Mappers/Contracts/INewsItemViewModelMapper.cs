namespace WhoCanHelpMe.Web.Controllers.Shared.Mappers.Contracts
{
    using Domain;

    using Framework.Mapper;

    using Home.ViewModels;

    public interface INewsItemViewModelMapper : IMapper<NewsItem, NewsItemViewModel>
    {
    }
}