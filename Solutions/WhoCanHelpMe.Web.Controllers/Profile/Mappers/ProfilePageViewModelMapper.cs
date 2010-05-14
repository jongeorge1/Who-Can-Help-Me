namespace WhoCanHelpMe.Web.Controllers.Profile.Mappers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Contracts;

    using Domain;

    using Framework.Mapper;

    using Shared.Mappers.Contracts;

    using ViewModels;

    using Profile=WhoCanHelpMe.Domain.Profile;

    #endregion

    public class ProfilePageViewModelMapper : IProfilePageViewModelMapper
    {
        private readonly ICategoryViewModelMapper categoryViewModelMapper;

        private readonly IPageViewModelBuilder pageViewModelBuilder;

        private readonly IProfileAssertionViewModelMapper profileAssertionViewModelMapper;

        public ProfilePageViewModelMapper(
            IPageViewModelBuilder pageViewModelBuilder,
            IProfileAssertionViewModelMapper profileAssertionViewModelMapper,
            ICategoryViewModelMapper categoryViewModelMapper)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
            this.profileAssertionViewModelMapper = profileAssertionViewModelMapper;
            this.categoryViewModelMapper = categoryViewModelMapper;
            Mapper.CreateMap<Profile, ProfilePageViewModel>();
        }

        public ProfilePageViewModel MapFrom(Profile input)
        {
            var viewModel = Mapper.Map<Profile, ProfilePageViewModel>(input);

            viewModel.Assertions =
                input.Assertions.OrderBy(a => a.Category.SortOrder.ToString("00000") + a.Tag.Name).ToList().MapAllUsing(
                    this.profileAssertionViewModelMapper);

            return this.pageViewModelBuilder.UpdateSiteProperties(viewModel);
        }

        public ProfilePageViewModel MapFrom(
            Profile input,
            IList<Category> categories)
        {
            var viewModel = this.MapFrom(input);

            this.AddCategoriesTo(
                viewModel,
                categories);

            return viewModel;
        }

        private void AddCategoriesTo(
            ProfilePageViewModel model,
            IList<Category> categories)
        {
            model.Categories = categories.OrderBy(c => c.SortOrder).ToList().MapAllUsing(this.categoryViewModelMapper);
        }
    }
}