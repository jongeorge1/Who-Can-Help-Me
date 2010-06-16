namespace MSpecTests.WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using System.Security.Authentication;
    using System.Web.Mvc;

    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Framework.Mapper;
    using global::WhoCanHelpMe.Framework.Security;
    using global::WhoCanHelpMe.Web.Controllers.Home;
    using global::WhoCanHelpMe.Web.Controllers.User;
    using global::WhoCanHelpMe.Web.Controllers.User.ViewModels;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Rhino.Mocks;
    using Machine.Specifications.Mvc;

    #endregion

    public abstract class specification_for_user_controller : Specification<UserController>
    {
        protected static IMapper<string, string, LoginPageViewModel> login_page_view_model_mapper;
        protected static IIdentityService identity_service;

        Establish context = () =>
            {
                identity_service = DependencyOf<IIdentityService>();
                login_page_view_model_mapper = DependencyOf<IMapper<string, string, LoginPageViewModel>>();
            };
    }

    [Subject(typeof(UserController))]
    public class when_the_user_controller_is_asked_for_the_default_view_and_the_user_is_logged_in : specification_for_user_controller
    {
        static ActionResult result;

        Establish context = () => identity_service.Stub(i => i.IsSignedIn()).Return(true);

        Because of = () => result = subject.Index();

        It should_ask_the_identity_service_if_the_user_is_signed_in =
            () => identity_service.AssertWasCalled(i => i.IsSignedIn());

        It should_redirect_to_home = () => result.ShouldRedirectToAction<HomeController>(x => x.Index());
    }

    [Subject(typeof(UserController))]
    public class when_the_user_controller_is_asked_for_the_default_view_and_the_user_is_not_logged_in : specification_for_user_controller
    {
        static ActionResult result;

        Establish context = () => identity_service.Stub(i => i.IsSignedIn()).Return(false);

        Because of = () => result = subject.Index();

        It should_ask_the_identity_service_if_the_user_is_signed_in =
            () => identity_service.AssertWasCalled(i => i.IsSignedIn());

        It should_redirect_to_the_login_action = () =>
            result.ShouldRedirectToAction<UserController>(x => x.Login(null));
    }

    [Subject(typeof(UserController))]
    public class when_the_user_controller_is_asked_for_the_login_view_and_the_user_is_logged_in : specification_for_user_controller
    {
        static ActionResult result;

        Establish context = () => identity_service.Stub(i => i.IsSignedIn()).Return(true);

        Because of = () => result = subject.Login(string.Empty);

        It should_ask_the_identity_service_if_the_user_is_signed_in =
            () => identity_service.AssertWasCalled(i => i.IsSignedIn());

        It should_redirect_to_home = () => result.ShouldRedirectToAction<HomeController>(x => x.Index());
    }

    [Subject(typeof(UserController))]
    public class when_the_user_controller_is_asked_for_the_login_view_and_the_user_is_not_logged_in : specification_for_user_controller
    {
        static ActionResult result;
        private static object the_view_model;

        Establish context = () => identity_service.Stub(i => i.IsSignedIn()).Return(false);

        Because of = () => result = subject.Login(string.Empty);

        It should_ask_the_identity_service_if_the_user_is_signed_in =
            () => identity_service.AssertWasCalled(i => i.IsSignedIn());

        It should_ask_the_login_page_view_model_mapper_to_map_the_view_model =
            () => login_page_view_model_mapper.AssertWasCalled(
                      m => m.MapFrom(
                               null,
                               string.Empty));

        It should_return_the_default_view = () => result.ShouldBeAView().And().ViewName.ShouldBeEmpty();

        It should_pass_the_view_model_to_the_view = () => 
            result.ShouldBeAView().And().ViewData.Model.ShouldEqual(the_view_model);
    }

    [Subject(typeof(UserController))]
    public class when_the_user_controller_is_asked_to_logout : specification_for_user_controller
    {
        static ActionResult result;

        Establish context = () => identity_service.Stub(i => i.IsSignedIn()).Return(true);

        Because of = () => result = subject.SignOut();

        It should_ask_the_identity_service_to_log_the_current_user_out =
            () => identity_service.AssertWasCalled(i => i.SignOut());

        It should_redirect_to_home = () => result.ShouldRedirectToAction<HomeController>(x => x.Index());
    }

    [Subject(typeof(UserController))]
    public class when_the_user_controller_is_asked_to_authenticate_with_a_return_url_and_authentication_is_successful : specification_for_user_controller
    {
        static ActionResult result;
        static string the_user_id;
        static string the_return_url;

        Establish context = () =>
        {
            the_user_id = "open id";
            the_return_url = "return url";
        };

        Because of = () => result = subject.Authenticate(the_user_id, the_return_url);

        It should_ask_the_identity_service_to_authenticate_the_user =
            () => identity_service.AssertWasCalled(i => i.Authenticate(the_user_id));

        It should_redirect_to_the_return_url = 
            () => result.ShouldBeARedirect().And().Url.ShouldEqual(the_return_url);
    }

    [Subject(typeof(UserController))]
    public class when_the_user_controller_is_asked_to_authenticate_without_a_return_url_and_authentication_is_successful : specification_for_user_controller
    {
        static ActionResult result;
        static string the_user_id;
        static string the_return_url;

        Establish context = () =>
        {
            the_user_id = "open id";
            the_return_url = string.Empty;
        };

        Because of = () => result = subject.Authenticate(the_user_id, the_return_url);

        It should_ask_the_identity_service_to_authenticate_the_user =
            () => identity_service.AssertWasCalled(i => i.Authenticate(the_user_id));

        It should_redirect_to_home = () => result.ShouldRedirectToAction<HomeController>(x => x.Index());
    }

    [Subject(typeof(UserController))]
    public class when_the_user_controller_is_asked_to_authenticate_and_authentication_is_unsuccessful : specification_for_user_controller
    {
        static ActionResult result;
        static string the_user_id;
        static string the_return_url;

        Establish context = () =>
        {
            the_user_id = "open id";
            the_return_url = "return url";

            identity_service.Stub(i => i.Authenticate(the_user_id)).Throw(new AuthenticationException());
        };

        Because of = () => result = subject.Authenticate(the_user_id, the_return_url);

        It should_ask_the_identity_service_to_authenticate_the_user =
            () => identity_service.AssertWasCalled(i => i.Authenticate(the_user_id));

        It should_redirect_to_the_login_view = () => 
            result.ShouldRedirectToAction<UserController>(x => x.Login(null));
    }
}
