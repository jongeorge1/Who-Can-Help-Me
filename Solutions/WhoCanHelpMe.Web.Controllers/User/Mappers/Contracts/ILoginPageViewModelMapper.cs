namespace WhoCanHelpMe.Web.Controllers.User.Mappers.Contracts
{
    #region Using Directives

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public interface ILoginPageViewModelMapper : IMapper<string, string, LoginPageViewModel>
    {
    }
}