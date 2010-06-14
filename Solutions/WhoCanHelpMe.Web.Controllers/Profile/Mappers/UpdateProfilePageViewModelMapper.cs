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

    public class UpdateProfilePageViewModelMapper : IMapper<Profile, IList<Category>, UpdateProfilePageViewModel>
    {
        private readonly IMapper<Category, CategoryViewModel> categoryViewModelMapper;

        private readonly IPageViewModelBuilder pageViewModelBuilder;

        private readonly IMapper<Assertion, ProfileAssertionViewModel> profileAssertionViewModelMapper;

        public UpdateProfilePageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IMapper<Assertion, ProfileAssertionViewModel> profileAssertionViewModelMapper,
            IMapper<Category, CategoryViewModel> categoryViewModelMapper)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
            this.profileAssertionViewModelMapper = profileAssertionViewModelMapper;
            this.categoryViewModelMapper = categoryViewModelMapper;
            Mapper.CreateMap<Profile, UpdateProfilePageViewModel>();
        }

        public UpdateProfilePageViewModel MapFrom(
            Profile input,
            IList<Category> categories)
        {
            var viewModel = Mapper.Map<Profile, UpdateProfilePageViewModel>(input);

            viewModel.Assertions = input.Assertions
                                        .OrderBy(a => a.Category.SortOrder.ToString("00000") + a.Tag.Name)
                                        .ToList()
                                        .MapAllUsing(this.profileAssertionViewModelMapper);

            this.AddCategoriesTo(
                viewModel,
                categories);

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }

        private void AddCategoriesTo(
            UpdateProfilePageViewModel model,
            IList<Category> categories)
        {
            model.Categories = categories.OrderBy(c => c.SortOrder).ToList().MapAllUsing(this.categoryViewModelMapper);
        }
    }
}