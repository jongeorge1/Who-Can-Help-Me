namespace WhoCanHelpMe.Web.Controllers.Profile
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using ActionFilters;

    using Domain;
    using Domain.Contracts.Tasks;

    using Extensions;

    using Home;

    using MvcContrib;
    using MvcContrib.Filters;

    using Shared.ActionResults;

    using ViewModels;

    using WhoCanHelpMe.Framework.Mapper;

    using xVal.ServerSide;

    #endregion

    public class ProfileController : BaseController
    {
        private readonly IMapper<AddAssertionFormModel, Identity, AddAssertionDetails> addAssertionDetailsMapper;

        private readonly ICategoryTasks categoryTasks;

        private readonly IMapper<CreateProfileFormModel, Identity, CreateProfileDetails> createProfileDetailsMapper;

        private readonly IMapper<CreateProfileDetails, CreateProfilePageViewModel> createProfilePageViewModelMapper;

        private readonly IIdentityTasks identityTasks;

        private readonly IMapper<Profile, ViewProfilePageViewModel> viewProfilePageViewModelMapper;

        private readonly IMapper<Profile, IList<Category>, UpdateProfilePageViewModel> updateProfilePageViewModelMapper;

        private readonly IProfileTasks userTasks;

        public ProfileController(
            IIdentityTasks identityTasks,
            IProfileTasks userTasks,
            ICategoryTasks categoryTasks,
            IMapper<Profile, ViewProfilePageViewModel> viewProfilePageViewModelMapper,
            IMapper<Profile, IList<Category>, UpdateProfilePageViewModel> updateProfilePageViewModelMapper,
            IMapper<AddAssertionFormModel, Identity, AddAssertionDetails> addAssertionDetailsMapper,
            IMapper<CreateProfileDetails, CreateProfilePageViewModel> createProfilePageViewModelMapper,
            IMapper<CreateProfileFormModel, Identity, CreateProfileDetails> createProfileDetailsMapper)
        {
            this.identityTasks = identityTasks;
            this.userTasks = userTasks;
            this.categoryTasks = categoryTasks;
            this.viewProfilePageViewModelMapper = viewProfilePageViewModelMapper;
            this.updateProfilePageViewModelMapper = updateProfilePageViewModelMapper;
            this.addAssertionDetailsMapper = addAssertionDetailsMapper;
            this.createProfilePageViewModelMapper = createProfilePageViewModelMapper;
            this.createProfileDetailsMapper = createProfileDetailsMapper;
        }

        [Authorize]
        [HttpGet]
        [ModelStateToTempData]
        [RequireNoExistingProfile("Profile", "Update")]
        public ActionResult Create()
        {
            var createProfileDetails = this.TempData.Get<CreateProfileDetails>();

            var viewModel = this.createProfilePageViewModelMapper.MapFrom(createProfileDetails);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateToTempData]
        [RequireNoExistingProfile("Profile", "Update")]
        public ActionResult Create(CreateProfileFormModel createProfile)
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            try
            {
                var createProfileDetails = this.createProfileDetailsMapper.MapFrom(
                    createProfile,
                    identity);

                this.userTasks.CreateProfile(createProfileDetails);

                return this.RedirectToAction(x => x.Update());
            }
            catch (RulesException ex)
            {
                ex.AddModelStateErrors(this.ModelState);
            }

            return this.RedirectToAction(x => x.Create());
        }

        [Authorize]
        [HttpGet]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult Delete()
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            this.userTasks.DeleteProfile(identity.UserName);

            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        [Authorize]
        [HttpGet]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult DeleteAssertion(int assertionId)
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            var user = this.userTasks.GetProfileByUserName(identity.UserName);

            this.userTasks.RemoveAssertion(
                user,
                assertionId);

            return this.RedirectToAction(x => x.Update());
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction(x => x.Update());
        }

        [Authorize]
        [HttpGet]
        [ModelStateToTempData]
        [RequireExistingProfile("Profile", "Create")]
        public ActionResult Update()
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            var user = this.userTasks.GetProfileByUserName(identity.UserName);

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
        public ActionResult Update(AddAssertionFormModel addAssertion)
        {
            var identity = this.identityTasks.GetCurrentIdentity();

            var addAssertionDetails = this.addAssertionDetailsMapper.MapFrom(
                addAssertion,
                identity);

            try
            {
                this.userTasks.AddAssertion(addAssertionDetails);
            }
            catch (RulesException ex)
            {
                ex.AddModelStateErrors(
                    this.ModelState,
                    "addAssertion");
            }

            return this.RedirectToAction(x => x.Update());
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