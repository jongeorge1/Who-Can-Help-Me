namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Domain;

    using Framework.Mapper;

    using Shared.Mappers.Contracts;

    using ViewModels;

    using Profile=WhoCanHelpMe.Domain.Profile;

    #endregion

    public class ViewProfilePageViewModelMapper : IMapper<Profile, ViewProfilePageViewModel>
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        private readonly IMapper<Assertion, ProfileAssertionViewModel> profileAssertionViewModelMapper;

        public ViewProfilePageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IMapper<Assertion, ProfileAssertionViewModel> profileAssertionViewModelMapper)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
            this.profileAssertionViewModelMapper = profileAssertionViewModelMapper;
            Mapper.CreateMap<Profile, ViewProfilePageViewModel>();
        }

        public ViewProfilePageViewModel MapFrom(Profile input)
        {
            var viewModel = Mapper.Map<Profile, ViewProfilePageViewModel>(input);

            viewModel.Assertions = input.Assertions
                                        .OrderBy(a => a.Category.SortOrder.ToString("00000") + a.Tag.Name)
                                        .ToList()
                                        .MapAllUsing(this.profileAssertionViewModelMapper);

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }
    }
}