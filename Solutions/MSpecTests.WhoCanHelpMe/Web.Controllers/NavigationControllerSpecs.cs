namespace MSpecTests.WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using System.Web.Mvc;

    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Framework.Security;
    using global::WhoCanHelpMe.Web.Controllers.Home;
    using global::WhoCanHelpMe.Web.Controllers.Navigation;
    using global::WhoCanHelpMe.Web.Controllers.Navigation.ViewModels;
    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Machine.Specifications.Mvc;
    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_navigation_controller : Specification<NavigationController>
    {
        protected static IIdentityService identity_service;

        Establish context = () => identity_service = DependencyOf<IIdentityService>();
    }

    [Subject(typeof(HomeController))]
    public class when_the_navigation_controller_is_asked_for_the_header : specification_for_navigation_controller
    {
        static ActionResult result;

        Establish context = () => identity_service.Stub(i => i.IsSignedIn()).Return(true);

        Because of = () => result = subject.Menu();

        It should_ask_the_identity_service_if_the_user_is_signed_in = () => 
            identity_service.AssertWasCalled(x => x.IsSignedIn());

        It should_return_the_default_view = () => result.ShouldBeAView().And().ViewName.ShouldBeEmpty();

        It should_not_use_a_master_page = () => result.ShouldBeAView().And().MasterName.ShouldBeEmpty();

        It should_set_the_view_model_property_to_a_new_menu_view_model = () =>
            result.Model<MenuViewModel>().ShouldNotBeNull();

        It should_set_the_properties_of_the_view_model_correctly = () => 
            result.Model<MenuViewModel>().IsLoggedIn.ShouldBeTrue();
    }
}
