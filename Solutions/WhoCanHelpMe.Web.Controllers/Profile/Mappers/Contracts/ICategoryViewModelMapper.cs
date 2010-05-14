namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers.Contracts
{
    #region Using Directives

    using Domain;

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public interface ICategoryViewModelMapper : IMapper<Category, CategoryViewModel>
    {
    }
}