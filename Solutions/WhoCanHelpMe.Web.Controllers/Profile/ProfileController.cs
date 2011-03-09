namespace WhoCanHelpMe.Web.Controllers.Profile
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using ActionFilters;

    using Domain;
    using Domain.Contracts.Tasks;

    using MvcContrib;
    using MvcContrib.Filters;

    using Shared.ActionResults;

    using SharpArch.Futures.Core.Mapping;

    using ViewModels;

    using WhoCanHelpMe.Framework.Security;
    using WhoCanHelpMe.Web.Controllers.Home;
    using WhoCanHelpMe.Web.Controllers.Profile.Mappers.Contracts;

    public class ProfileController : BaseController
    {
        private readonly ICategoryTasks categoryTasks;

        private readonly IIdentityService identityService;

        private readonly ICreateProfilePageViewModelBuilder createProfilePageViewModelBuilder;

        private readonly IMapper<Profile, ViewProfilePageViewModel> viewProfilePageViewModelMapper;

        private readonly IMapper<Profile, IList<Category>, UpdateProfilePageViewModel> updateProfilePageViewModelMapper;

        private readonly IProfileTasks userTasks;

        public ProfileController(
            IIdentityService identityService,
            IProfileTasks userTasks,
            ICategoryTasks categoryTasks,
            IMapper<Profile, ViewProfilePageViewModel> viewProfilePageViewModelMapper,
            IMapper<Profile, IList<Category>, UpdateProfilePageViewModel> updateProfilePageViewModelMapper, 
            ICreateProfilePageViewModelBuilder createProfilePageViewModelBuilder)
        {
            this.identityService = identityService;
            this.createProfilePageViewModelBuilder = createProfilePageViewModelBuilder;
            this.userTasks = userTasks;
            this.categoryTasks = categoryTasks;
            this.viewProfilePageViewModelMapper = viewProfilePageViewModelMapper;
            this.updateProfilePageViewModelMapper = updateProfilePageViewModelMapper;
        }

        [Authorize]
        [HttpGet]
        [ModelStateToTempData]
        [RequireNoExistingProfile("Profile", "Update")]
        public ActionResult Create()
        {
            var viewModel = this.createProfilePageViewModelBuilder.Get();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateToTempData]
        [RequireNoExistingProfile("Profile", "Update")]
        public ActionResult Create(CreateProfileFormModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                var identity = this.identityService.GetCurrentIdentity();

                this.userTasks.CreateProfile(identity.Name, formModel.FirstName, formModel.LastName);

                return this.RedirectToAction(c => c.Update());
            }

            return this.RedirectToAction(c => c.Create());
        }

        [Authorize]
        [HttpGet]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult Delete()
        {
            var identity = this.identityService.GetCurrentIdentity();

            this.userTasks.DeleteProfile(identity.Name);

            return this.RedirectToAction<HomeController>(c => c.Index());
        }

        [Authorize]
        [HttpGet]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult DeleteAssertion(int assertionId)
        {
            var identity = this.identityService.GetCurrentIdentity();

            var user = this.userTasks.GetProfileByUserName(identity.Name);

            this.userTasks.RemoveAssertion(
                user,
                assertionId);

            return this.RedirectToAction(c => c.Update());
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction(c => c.Update());
        }

        [Authorize]
        [HttpGet]
        [ModelStateToTempData]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult Update()
        {
            var identity = this.identityService.GetCurrentIdentity();

            var user = this.userTasks.GetProfileByUserName(identity.Name);

            var categories = this.categoryTasks.GetAll();

            var viewModel = this.updateProfilePageViewModelMapper.MapFrom(
                user,
                categories);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateToTempData]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult Update(AddAssertionFormModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                var identity = this.identityService.GetCurrentIdentity();

                this.userTasks.AddAssertion(identity.Name, formModel.CategoryId, formModel.TagName);
            }

            return this.RedirectToAction(c => c.Update());
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            var user = this.userTasks.GetProfileById(id);

            if (user != null)
            {
                var profileViewModel = this.viewProfilePageViewModelMapper.MapFrom(user);

                return this.View(profileViewModel);
            }

            return new NotFoundResult();
        }
    }
}