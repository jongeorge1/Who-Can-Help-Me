namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers.Contracts
{
    #region Using Directives

    using System.Collections.Generic;

    using Domain;

    using Framework.Mapper;

    using ViewModels;

    #endregion

    public interface IProfilePageViewModelMapper : IMapper<Profile, ProfilePageViewModel>
    {
        ProfilePageViewModel MapFrom(
            Profile input,
            IList<Category> categories);
    }
}