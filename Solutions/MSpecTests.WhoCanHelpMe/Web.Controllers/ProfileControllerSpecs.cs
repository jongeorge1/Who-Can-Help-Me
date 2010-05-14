namespace MSpecTests.WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Web.Controllers.Home;
    using global::WhoCanHelpMe.Web.Controllers.Profile;
    using global::WhoCanHelpMe.Web.Controllers.Profile.Mappers.Contracts;
    using global::WhoCanHelpMe.Web.Controllers.Profile.ViewModels;
    using global::WhoCanHelpMe.Web.Controllers.Shared.ActionResults;
    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Rhino.Mocks;

    using xVal.ServerSide;
    using Machine.Specifications.Mvc;

    #endregion;

    public abstract class specification_for_profile_controller : Specification<ProfileController>
    {
        protected static IProfileTasks user_tasks;
        protected static IIdentityTasks identity_tasks;
        protected static ICategoryTasks category_tasks;
        protected static IProfilePageViewModelMapper profile_view_model_mapper;
        protected static ITagTasks tag_tasks;
        protected static IAddAssertionDetailsMapper add_assertion_details_mapper;
        protected static ICreateProfilePageViewModelMapper create_profile_page_view_model_mapper;
        protected static ICreateProfileDetailsMapper create_profile_details_mapper;
            
        Establish context = () =>
            {
                identity_tasks = DependencyOf<IIdentityTasks>();
                user_tasks = DependencyOf<IProfileTasks>();
                category_tasks = DependencyOf<ICategoryTasks>();
                profile_view_model_mapper = DependencyOf<IProfilePageViewModelMapper>();
                add_assertion_details_mapper = DependencyOf<IAddAssertionDetailsMapper>();
                create_profile_page_view_model_mapper = DependencyOf<ICreateProfilePageViewModelMapper>();
                create_profile_details_mapper = DependencyOf<ICreateProfileDetailsMapper>();
            };
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_asked_for_the_create_view : specification_for_profile_controller
    {
        static ActionResult result;
        static CreateProfilePageViewModel the_view_model;

        Establish context = () =>
            {
                the_view_model = new CreateProfilePageViewModel();

                create_profile_page_view_model_mapper.Stub(m => m.MapFrom(null)).Return(the_view_model);
            };

        Because of = () => result = subject.Create();

        It should_ask_the_create_profile_page_view_model_mapper_to_map_the_view_model =
            () => create_profile_page_view_model_mapper.AssertWasCalled(m => m.MapFrom(null));

        It should_return_the_default_view =
            () => result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_pass_the_view_model_to_the_view =
            () => result.Model<CreateProfilePageViewModel>().ShouldBeTheSameAs(the_view_model);

    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_asked_for_the_create_view_after_a_failed_submission_has_occurred : specification_for_profile_controller
    {
        static ActionResult result;
        static CreateProfileDetails create_profile_details;
        static CreateProfilePageViewModel the_view_model;
        static ITempDataProvider temp_data_provider;
        static IDictionary<string, object> temp_data_dictionary;

        Establish context = () =>
        {
            the_view_model = new CreateProfilePageViewModel();

            temp_data_provider = MockRepository.GenerateStub<ITempDataProvider>();
            temp_data_dictionary = new Dictionary<string, object>();
            temp_data_dictionary.Add(typeof(CreateProfileDetails).Name, create_profile_details);

            create_profile_page_view_model_mapper.Stub(m => m.MapFrom(create_profile_details)).Return(the_view_model);

            temp_data_provider.Stub(t => t.LoadTempData(subject.ControllerContext)).Return(temp_data_dictionary);
        };

        Because of = () => result = subject.Create();

        It should_ask_the_create_profile_page_view_model_mapper_to_map_the_view_model =
            () => create_profile_page_view_model_mapper.AssertWasCalled(m => m.MapFrom(create_profile_details));

        It should_return_the_default_view =
            () => result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_pass_the_view_model_to_the_view =
            () => result.Model<CreateProfilePageViewModel>().ShouldBeTheSameAs(the_view_model);
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_told_to_create_a_new_profile : specification_for_profile_controller
    {
        static string user_name;
        static Identity the_identity;
        static ActionResult result;
        static CreateProfileFormViewModel create_profile_view_model;
        static CreateProfileDetails create_profile_details;

        Establish context = () =>
            {
                user_name = "user_name";

                the_identity = new Identity {
                                                UserName = user_name
                                            };

                create_profile_view_model = new CreateProfileFormViewModel();
                create_profile_details = new CreateProfileDetails();

                identity_tasks.Stub(i => i.GetCurrentIdentity()).Return(the_identity);
                create_profile_details_mapper.Stub(m => m.MapFrom(create_profile_view_model, the_identity)).Return(create_profile_details);
            };

        Because of = () => result = subject.Create(create_profile_view_model);

        It should_ask_the_identity_tasks_for_the_logged_in_user =
            () => identity_tasks.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_create_profile_details_mapper_to_map_from_the_submitted_view_model =
            () => create_profile_details_mapper.AssertWasCalled(m => m.MapFrom(create_profile_view_model, the_identity));

        It should_ask_the_user_tasks_to_create_the_new_profile =
            () => user_tasks.AssertWasCalled(u => u.CreateProfile(create_profile_details));

        It should_redirect_to_the_update_action =
            () => result.ShouldRedirectToAction<ProfileController>(x => x.Update()); 
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_told_to_create_a_new_profile_with_invalid_data : specification_for_profile_controller
    {
        static string user_name;
        static Identity the_identity;
        static ActionResult result;
        static CreateProfileFormViewModel create_profile_view_model;
        static CreateProfileDetails create_profile_details;

        Establish context = () =>
        {
            user_name = "user_name";

            the_identity = new Identity
            {
                UserName = user_name
            };

            create_profile_view_model = new CreateProfileFormViewModel();
            create_profile_details = new CreateProfileDetails();

            identity_tasks.Stub(i => i.GetCurrentIdentity()).Return(the_identity);
            create_profile_details_mapper.Stub(m => m.MapFrom(create_profile_view_model, the_identity)).Return(create_profile_details);
            user_tasks.Stub(u => u.CreateProfile(create_profile_details)).Throw(new RulesException(new List<ErrorInfo>()));
        };

        Because of = () => result = subject.Create(create_profile_view_model);

        It should_ask_the_identity_tasks_for_the_logged_in_user =
            () => identity_tasks.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_create_profile_details_mapper_to_map_from_the_submitted_view_model =
            () => create_profile_details_mapper.AssertWasCalled(m => m.MapFrom(create_profile_view_model, the_identity));

        It should_ask_the_user_tasks_to_create_the_new_profile =
            () => user_tasks.AssertWasCalled(u => u.CreateProfile(create_profile_details));

        It should_redirect_to_the_create_action =
            () => result.ShouldRedirectToAction<ProfileController>(x => x.Create());
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_asked_to_delete_a_profile : specification_for_profile_controller
    {
        static Identity the_identity;
        static ActionResult result;
        static string user_name;

        Establish context = () =>
            {
                the_identity = new Identity
                    {
                        UserName = user_name
                    };

                identity_tasks.Stub(i => i.GetCurrentIdentity()).Return(the_identity);
            };

        Because b = () => result = subject.Delete();

        It should_ask_the_identity_tasks_for_the_logged_in_user =
            () => identity_tasks.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_user_tasks_to_delete_the_profile =
            () => user_tasks.AssertWasCalled(u => u.DeleteProfile(user_name));

        It should_redirect_to_home =
            () => result.ShouldRedirectToAction<HomeController>(x => x.Index());
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_asked_to_delete_an_assertion : specification_for_profile_controller
    {
        static ActionResult result;
        static Profile the_retrieved_profile;
        static int the_assertion_id;
        static string the_user_name;
        static Identity the_current_identity;

        Establish context = () =>
        {
            the_user_name = "domain\account";

            the_current_identity = new Identity
            {
                UserName = the_user_name
            };

            the_retrieved_profile = new Profile();

            the_assertion_id = 100;

            identity_tasks.Stub(it => it.GetCurrentIdentity()).Return(the_current_identity);

            user_tasks.Stub(ut => ut.GetProfileByUserName(the_user_name)).Return(the_retrieved_profile);
        };

        Because of = () => result = subject.DeleteAssertion(the_assertion_id);

        It should_ask_the_identity_tasks_for_the_current_identity =
            () => identity_tasks.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_user_tasks_for_the_user =
            () => user_tasks.AssertWasCalled(u => u.GetProfileByUserName(the_user_name));

        It should_tell_the_user_tasks_to_remove_the_assertion =
            () => user_tasks.AssertWasCalled(u => u.RemoveAssertion(the_retrieved_profile, the_assertion_id));

        It should_redirect_to_the_update_action =
            () => result.ShouldRedirectToAction<ProfileController>(x => x.Update());
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_asked_for_the_default_action : specification_for_profile_controller
    {
        static ActionResult result;

        Because of = () => result = subject.Index();

        It should_redirect_to_the_update_action = () => result.ShouldRedirectToAction<ProfileController>(x => x.Update());
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_asked_for_the_update_view : specification_for_profile_controller
    {
        static string the_user_name;
        static Identity the_current_identity;
        static Profile the_retrieved_profile;
        static List<Category> all_categories;
        static ActionResult result;
        static ProfilePageViewModel the_view_model;

        Establish context = () =>
        {
            the_user_name = "domain\account";

            the_current_identity = new Identity
            {
                UserName = the_user_name
            };

            the_retrieved_profile = new Profile();

            the_view_model = new ProfilePageViewModel();

            user_tasks.Stub(ut => ut.GetProfileByUserName(the_user_name)).Return(the_retrieved_profile);

            all_categories = new List<Category>();

            category_tasks.Stub(ct => ct.GetAll()).Return(all_categories);

            identity_tasks.Stub(it => it.GetCurrentIdentity()).Return(the_current_identity);

            profile_view_model_mapper.Stub(pvmm => pvmm.MapFrom(the_retrieved_profile, all_categories)).Return(the_view_model);
        };

        Because of = () => result = subject.Update();

        It should_ask_the_identity_tasks_for_the_current_identity =
            () => identity_tasks.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_user_tasks_for_the_user =
            () => user_tasks.AssertWasCalled(u => u.GetProfileByUserName(the_user_name));

        It should_ask_the_category_tasks_for_all_categories =
            () => category_tasks.AssertWasCalled(c => c.GetAll());

        It should_ask_the_profile_view_model_mapper_to_map_the_user_and_categories =
            () => profile_view_model_mapper.AssertWasCalled(m => m.MapFrom(the_retrieved_profile, all_categories));

        It should_return_the_profile_view = () =>
            result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_pass_the_view_model_to_the_profile_view =
            () => result.Model<ProfilePageViewModel>().ShouldBeTheSameAs(the_view_model);
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_told_to_update_a_profile : specification_for_profile_controller
    {
        static ActionResult result;
        static AddAssertionFormViewModel the_view_model;
        static Identity the_identity;
        static AddAssertionDetails the_add_assertion_details;

        Establish context = () =>
            {
                the_view_model = new AddAssertionFormViewModel();

                the_identity = new Identity();

                the_add_assertion_details = new AddAssertionDetails();

                identity_tasks.Stub(i => i.GetCurrentIdentity()).Return(the_identity);
                add_assertion_details_mapper.Stub(
                    m => m.MapFrom(
                             the_view_model,
                             the_identity)).Return(the_add_assertion_details);
            };

        Because of = () => result = subject.Update(the_view_model);

        It should_ask_the_identity_tasks_for_the_current_identity =
            () => identity_tasks.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_add_assertion_details_mapper_to_map_from_the_submitted_view_model_and_identity =
            () => add_assertion_details_mapper.AssertWasCalled(
                      m => m.MapFrom(
                               the_view_model,
                               the_identity));

        It should_ask_the_user_tasks_to_add_the_new_assertion =
            () => user_tasks.AssertWasCalled(u => u.AddAssertion(the_add_assertion_details));

        It should_redirect_to_the_update_action =
            () => result.ShouldRedirectToAction<ProfileController>(x => x.Update());
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_told_to_update_a_profile_with_invalid_data : specification_for_profile_controller
    {
        static ActionResult result;
        static AddAssertionFormViewModel the_view_model;
        static Identity the_identity;
        static AddAssertionDetails the_add_assertion_details;

        Establish context = () =>
        {
            the_view_model = new AddAssertionFormViewModel();

            the_identity = new Identity();

            the_add_assertion_details = new AddAssertionDetails();

            identity_tasks.Stub(i => i.GetCurrentIdentity()).Return(the_identity);
            
            add_assertion_details_mapper.Stub(
                m => m.MapFrom(
                         the_view_model,
                         the_identity)).Return(the_add_assertion_details);

            user_tasks.Stub(u => u.AddAssertion(the_add_assertion_details)).Throw(
                new RulesException(new List<ErrorInfo>()));
        };

        Because of = () => result = subject.Update(the_view_model);

        It should_ask_the_identity_tasks_for_the_current_identity =
            () => identity_tasks.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_add_assertion_details_mapper_to_map_from_the_submitted_view_model_and_identity =
            () => add_assertion_details_mapper.AssertWasCalled(
                      m => m.MapFrom(
                               the_view_model,
                               the_identity));

        It should_ask_the_user_tasks_to_add_the_new_assertion =
            () => user_tasks.AssertWasCalled(u => u.AddAssertion(the_add_assertion_details));

        It should_redirect_to_the_update_action =
            () => result.ShouldRedirectToAction<ProfileController>(x => x.Update());
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_asked_for_a_specific_profile : specification_for_profile_controller
    {
        static int the_user_id;
        static Profile the_retrieved_profile;
        static ActionResult result;
        static ProfilePageViewModel the_view_model;

        Establish context = () =>
        {
            the_user_id = 10;

            the_retrieved_profile = new Profile();

            user_tasks.Stub(ut => ut.GetProfileById(the_user_id)).Return(the_retrieved_profile);

            the_view_model = new ProfilePageViewModel();

            profile_view_model_mapper.Stub(pvmm => pvmm.MapFrom(the_retrieved_profile)).Return(the_view_model);
        };

        Because of = () => result = subject.View(the_user_id);

        It should_ask_the_user_tasks_for_the_user =
            () => user_tasks.AssertWasCalled(u => u.GetProfileById(the_user_id));

        It should_ask_the_profile_view_model_mapper_to_map_the_user =
            () => profile_view_model_mapper.AssertWasCalled(m => m.MapFrom(the_retrieved_profile));

        It should_return_the_default_view = () =>
            result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_pass_the_view_model_to_the_profile_view =
            () => result.Model<ProfilePageViewModel>().ShouldBeTheSameAs(the_view_model);
    }

    [Subject(typeof(ProfileController))]
    public class when_the_profile_controller_is_asked_for_a_specific_profile_but_the_profile_does_not_exist : specification_for_profile_controller
    {
        static ActionResult result;
        static int the_user_id;

        Establish context = () =>
            {
                the_user_id = 10;

                user_tasks.Stub(ut => ut.GetProfileById(the_user_id)).Return(null);
            };

        Because of = () => result = subject.View(the_user_id);

        It should_ask_the_user_tasks_for_the_profile = () => user_tasks.AssertWasCalled(u => u.GetProfileById(the_user_id));

        It should_return_a_not_found_result = () => result.ShouldBeOfType<NotFoundResult>();
    }
}
