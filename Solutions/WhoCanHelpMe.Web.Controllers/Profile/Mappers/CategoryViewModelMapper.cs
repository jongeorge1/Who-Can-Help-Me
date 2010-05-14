namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    #region Using Directives

    using Contracts;

    using Domain;

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public class CategoryViewModelMapper : BaseMapper<Category, CategoryViewModel>,
                                           ICategoryViewModelMapper
    {
    }
}