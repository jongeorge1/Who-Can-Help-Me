//-------------------------------------------------------------------------------------------------
// <auto-generated> 
// Marked as auto-generated so StyleCop will ignore BDD style tests
// </auto-generated>
//-------------------------------------------------------------------------------------------------

#pragma warning disable 169
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace MSpecTests.WhoCanHelpMe.Presentation.Controllers
{
    #region Using Directives

    using System;
    using System.Web.Mvc;
    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Framework.Security;
    using global::WhoCanHelpMe.Presentation.Controllers.Profile.ActionFilters;
    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Machine.Specifications.Mvc;
    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_require_existing_profile_attribute : Specification<RequireExistingProfileAttribute>
    {
        protected static ActionExecutingContext filter_context;
        protected static IProfileTasks profile_tasks;
        protected static IIdentityService identity_service;
        protected static string redirect_controller_name;
        protected static string redirect_action_name;

        Establish context = () =>
            {
                ServiceLocatorHelper.InitialiseServiceLocator();

                identity_service = An<IIdentityService>().AddToServiceLocator();
                profile_tasks = An<IProfileTasks>().AddToServiceLocator();

                filter_context = new ActionExecutingContext();
            };

        protected override RequireExistingProfileAttribute CreateSubject()
        {
            return new RequireExistingProfileAttribute(
                redirect_controller_name,
                redirect_action_name);
        }
    }

    [Subject(typeof(RequireExistingProfileAttribute))]
    public class when_the_require_existing_profile_attribute_is_used_and_the_user_has_a_profile : specification_for_require_existing_profile_attribute
    {
        static Identity the_identity;
        static Profile the_profile;
        static string user_name;

        Establish context = () =>
            {
                user_name = "username";

                the_identity = new Identity(user_name, string.Empty, true);

                the_profile = new Profile();

                redirect_controller_name = "controller";
                redirect_action_name = "action";

                identity_service.Stub(i => i.GetCurrentIdentity()).Return(the_identity);
                profile_tasks.Stub(p => p.GetProfileByUserName(user_name)).Return(the_profile);
            };

        Because of = () => subject.OnActionExecuting(filter_context);

        It should_ask_the_identity_service_for_the_current_identity = () => identity_service.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_profile_tasks_for_the_current_user_profile = () => profile_tasks.AssertWasCalled(p => p.GetProfileByUserName(user_name));

        It should_not_redirect = () => filter_context.Result.ShouldBeNull();
    }

    [Subject(typeof(RequireExistingProfileAttribute))]
    public class when_the_require_existing_profile_attribute_is_used_and_the_user_does_not_have_a_profile : specification_for_require_existing_profile_attribute
    {
        static Identity the_identity;
        static string user_name;

        Establish context = () =>
        {
            user_name = "username";

            the_identity = new Identity(user_name, string.Empty, true);

            redirect_controller_name = "controller";
            redirect_action_name = "action";

            identity_service.Stub(i => i.GetCurrentIdentity()).Return(the_identity);
            profile_tasks.Stub(p => p.GetProfileByUserName(user_name)).Return(null);
        };

        Because of = () => subject.OnActionExecuting(filter_context);

        It should_ask_the_identity_service_for_the_current_identity = () => identity_service.AssertWasCalled(i => i.GetCurrentIdentity());

        It should_ask_the_profile_tasks_for_the_current_user_profile = () => profile_tasks.AssertWasCalled(p => p.GetProfileByUserName(user_name));

        It should_redirect = () =>
            {
                filter_context.Result.ShouldBeARedirectToRoute().And().ControllerName().ShouldEqual(redirect_controller_name);
                filter_context.Result.ShouldBeARedirectToRoute().And().ActionName().ShouldEqual(redirect_action_name);
            };
    }
}