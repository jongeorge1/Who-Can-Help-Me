namespace MSpecTests.WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Repositories;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Domain.Specifications;
    using global::WhoCanHelpMe.Tasks;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Rhino.Mocks;

    using SharpArch.Core;

    using xVal.ServerSide;

    #endregion

    public abstract class specification_for_profile_tasks : Specification<IProfileTasks, ProfileTasks>
    {
        protected static IProfileRepository the_profile_repository;
        protected static ITagRepository the_tag_repository;
        protected static ICategoryRepository the_category_repository;

        Establish context = () =>
            {
                the_profile_repository = DependencyOf<IProfileRepository>();
                the_tag_repository = DependencyOf<ITagRepository>();
                the_category_repository = DependencyOf<ICategoryRepository>();

                ServiceLocatorHelper.InitialiseServiceLocator();
                ServiceLocatorHelper.AddValidator();
            };
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_get_a_profile_by_user_name : specification_for_profile_tasks
    {
        static Profile result;
        static string user_name;
        static Profile the_profile;

        Establish context = () =>
            {
                user_name = "user_name";

                the_profile = new Profile();

                the_profile_repository.StubFindOne().Return(the_profile);
            };

        Because of = () => result = subject.GetProfileByUserName(user_name);

        It should_ask_the_repository_for_the_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByUserNameSpecification, Profile>(spec => spec.UserName == user_name);

        It should_return_the_profile_it_retrieved_from_the_repository = () => result.ShouldBeTheSameAs(the_profile);
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_get_a_profile_by_user_name_and_there_is_no_matching_profile : specification_for_profile_tasks
    {
        static Profile result;
        static string user_name;

        Establish context = () =>
        {
            user_name = "user_name";
            the_profile_repository.StubFindOne().Return(null);
        };

        Because of = () => result = subject.GetProfileByUserName(user_name);

        It should_ask_the_repository_for_the_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByUserNameSpecification, Profile>(spec => spec.UserName == user_name);

        It should_return_null = () => result.ShouldBeNull();
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_get_a_profile_by_profile_id : specification_for_profile_tasks
    {
        static Profile result;
        static int profile_id;
        static Profile the_profile;

        Establish context = () =>
        {
            profile_id = 1;

            the_profile = new Profile();

            the_profile_repository.StubFindOne().Return(the_profile);
        };

        Because of = () => result = subject.GetProfileById(profile_id);

        It should_ask_the_repository_for_the_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByIdSpecification, Profile>(spec => spec.Id == profile_id);

        It should_return_the_profile_it_retrieved_from_the_repository = () => result.ShouldBeTheSameAs(the_profile);
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_get_a_profile_by_profile_id_and_there_is_no_matching_profile : specification_for_profile_tasks
    {
        static Profile result;
        static int profile_id;
        static ProfileByIdSpecification specification;

        Establish context = () =>
        {
            profile_id = 1;

            the_profile_repository.StubFindOne().Return(null);
        };

        Because of = () => result = subject.GetProfileById(profile_id);

        It should_ask_the_repository_for_the_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByIdSpecification, Profile>(spec => spec.Id == profile_id);

        It should_return_null = () => result.ShouldBeNull();
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_remove_an_assertion : specification_for_profile_tasks
    {
        static Profile the_profile;
        static int the_assertion_id;
        static Assertion the_assertion;

        Establish context = () =>
        {
            the_assertion_id = 10;

            the_assertion = new Assertion();
            the_assertion.SetNonPublicProperty(a => a.Id, the_assertion_id);

            the_profile = new Profile
            {
                Assertions = new List<Assertion> { the_assertion }
            };
        };

        Because of = () => subject.RemoveAssertion(the_profile, the_assertion_id);

        It should_remove_the_assertion_from_the_profile =
            () => the_profile.Assertions.Count.ShouldEqual(0);

        It should_ask_the_profile_repository_to_save_the_profile =
            () => the_profile_repository.AssertWasCalled(pr => pr.Save(the_profile));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_remove_an_assertion_that_does_not_belong_to_the_specified_profile : specification_for_profile_tasks
    {
        static Profile the_profile;
        static int the_assertion_id;

        Establish context = () =>
        {
            the_assertion_id = 10;

            the_profile = new Profile
            {
                Assertions = new List<Assertion> {  }
            };
        };

        Because of = () => subject.RemoveAssertion(the_profile, the_assertion_id);

        It should_do_nothing = 
            () => the_profile_repository.AssertWasNotCalled(pr => pr.Save(the_profile));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_add_an_assertion_with_an_existing_tag_name : specification_for_profile_tasks
    {
        static Profile the_profile;
        static Tag the_tag;
        static Category the_category;
        static string the_tag_name;
        static string the_user_name;
        static int the_category_id;

        Establish context = () =>
            {
                the_user_name = "user name";
                the_profile = new Profile();

                the_tag = new Tag();
                the_tag_name = "my tag";

                the_category = new Category();
                the_category_id = 10;

                the_profile_repository.StubFindOne().Return(the_profile);

                the_category_repository.StubFindOne().Return(the_category);

                the_tag_repository.StubFindOne().Return(the_tag);
            };

        Because of = () => subject.AddAssertion(the_user_name, the_category_id, the_tag_name);

        It should_ask_the_profile_repository_for_the_users_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByUserNameSpecification, Profile>(spec => spec.UserName == the_user_name);

        It should_ask_the_tag_repository_for_the_tag_to_add = () => the_tag_repository.AssertFindOneWasCalledWithSpecification<TagByNameSpecification, Tag>(spec => spec.Name == the_tag_name);

        It should_ask_the_category_repository_for_the_matching_category = () => the_category_repository.AssertFindOneWasCalledWithSpecification<CategoryByIdSpecification, Category>(spec => spec.Id == the_category_id);

        It should_add_the_new_assertion_to_the_profile =
            () => the_profile.Assertions.Count.ShouldEqual(1);

        It should_ask_the_profile_repository_to_save_the_profile =
            () => the_profile_repository.AssertWasCalled(pr => pr.Save(the_profile));

        It should_not_modify_the_existing_tag =
            () => the_tag_repository.AssertWasNotCalled(tr => tr.Save(null));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_add_an_assertion_with_a_new_tag_name : specification_for_profile_tasks
    {
        static int the_category_id;
        static string the_tag_name;
        static string the_user_name;
        static Category the_category;
        static Profile the_profile;

        Establish context = () =>
            {
                the_user_name = "user name";
                the_profile = new Profile();

                the_tag_name = "my tag";

                the_category = new Category();
                the_category_id = 10;

                the_profile_repository.StubFindOne().Return(the_profile);

                the_category_repository.StubFindOne().Return(the_category);

                the_tag_repository.StubFindOne().Return(null);
            };

        Because of = () => subject.AddAssertion(the_user_name, the_category_id, the_tag_name);
        
        It should_ask_the_profile_repository_for_the_users_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByUserNameSpecification, Profile>(spec => spec.UserName == the_user_name);

        It should_ask_the_tag_repository_for_the_tag_to_add = () => the_tag_repository.AssertFindOneWasCalledWithSpecification<TagByNameSpecification, Tag>(spec => spec.Name == the_tag_name);

        It should_ask_the_category_repository_for_the_matching_category = () => the_category_repository.AssertFindOneWasCalledWithSpecification<CategoryByIdSpecification, Category>(spec => spec.Id == the_category_id);

        It should_add_the_new_assertion_to_the_profile = () =>
            {
                the_profile.Assertions.Count.ShouldEqual(1);
                the_profile.Assertions[0].Tag.Name.ShouldEqual(the_tag_name);
                the_profile.Assertions[0].Category.ShouldBeTheSameAs(the_category);
                the_profile.Assertions[0].Profile.ShouldBeTheSameAs(the_profile);
            };

        It should_ask_the_profile_repository_to_save_the_profile = () => the_profile_repository.AssertWasCalled(pr => pr.Save(the_profile));

        It should_ask_the_tag_repository_to_save_the_new_tag = () =>
            {
                var arguments = the_tag_repository.GetArgumentsForCallsMadeOn(p => p.Save(null), m => m.IgnoreArguments());

                arguments.Count.ShouldEqual(1);
                arguments.First().First().ShouldBe(typeof(Tag));

                var tag = arguments[0][0] as Tag;
                tag.Name.ShouldEqual(the_tag_name);
            };
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_add_an_assertion_without_specifying_a_tag_name : specification_for_profile_tasks
    {
        static string the_user_name;
        static string the_tag_name;
        static int the_category_id;
        static Exception the_exception;

        Establish context = () => 
        {
            the_user_name = "user name";
            the_tag_name = string.Empty;
            the_category_id = 1;
        };

        Because of = () => the_exception = Catch.Exception(() => subject.AddAssertion(the_user_name, the_category_id, the_tag_name));

        It should_throw_a_precondition_exception_containing_the_error = () => the_exception.ShouldBe(typeof(PreconditionException));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_add_an_assertion_without_specifying_a_user_name : specification_for_profile_tasks
    {
        static string the_user_name;
        static string the_tag_name;
        static int the_category_id;
        static Exception the_exception;

        Establish context = () =>
        {
            the_user_name = string.Empty;
            the_tag_name = "tag name";
            the_category_id = 1;
        };

        Because of = () => the_exception = Catch.Exception(() => subject.AddAssertion(the_user_name, the_category_id, the_tag_name));

        It should_throw_a_precondition_exception_containing_the_error = () => the_exception.ShouldBe(typeof(PreconditionException));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_add_an_assertion_with_an_invalid_category_id : specification_for_profile_tasks
    {
        static Exception the_exception;
        static string the_user_name;
        static string the_tag_name;
        static int the_category_id;
        static Profile the_profile;

        Establish context = () =>
        {
            the_category_id = 1;
            the_tag_name = "tag name";
            the_user_name = "user";

            the_profile = new Profile();

            the_profile_repository.StubFindOne().Return(the_profile);

            the_category_repository.StubFindOne().Return(null);
        };

        Because of = () => the_exception = Catch.Exception(() => subject.AddAssertion(the_user_name, the_category_id, the_tag_name));

        It should_throw_a_postcondition_exception_containing_the_error = () => the_exception.ShouldBe(typeof(PostconditionException));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_create_a_profile : specification_for_profile_tasks
    {
        static string the_first_name;
        static string the_last_name;
        static string the_user_name;
        static Profile dummy_profile;

        Establish context = () =>
            {
                dummy_profile = new Profile();
                the_first_name = "First name";
                the_last_name = "Last name";
                the_user_name = "User name";
            };

        Because of = () => subject.CreateProfile(the_user_name, the_first_name, the_last_name);

        It should_create_a_new_profile_based_on_the_create_profile_details = () =>
            {
                var arguments = the_profile_repository.GetArgumentsForCallsMadeOn(
                    p => p.Save(dummy_profile),
                    o => o.IgnoreArguments());

                arguments.First().First().ShouldBe(typeof(Profile));

                var created_profile = arguments.First().First() as Profile;

                created_profile.FirstName.ShouldEqual(the_first_name);
                created_profile.LastName.ShouldEqual(the_last_name);
                created_profile.UserName.ShouldEqual(the_user_name);
            };

        It should_ask_the_profile_repository_to_save_the_new_profile = () => the_profile_repository.AssertWasCalled(
                                                                                 p => p.Save(dummy_profile),
                                                                                 o => o.IgnoreArguments());

    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_create_a_profile_without_specifying_a_user_name : specification_for_profile_tasks
    {
        static string the_first_name;
        static string the_last_name;
        static string the_user_name;
        static Exception the_exception;

        Establish context = () =>
        {
            the_first_name = "First name";
            the_last_name = "Last name";
            the_user_name = string.Empty;
        };

        Because of = () => the_exception = Catch.Exception(() => subject.CreateProfile(the_user_name, the_first_name, the_last_name));

        It should_throw_a_precondition_exception_containing_the_error = () => the_exception.ShouldBe(typeof(PreconditionException));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_create_a_profile_without_specifying_a_first_name : specification_for_profile_tasks
    {
        static string the_first_name;
        static string the_last_name;
        static string the_user_name;
        static Exception the_exception;

        Establish context = () =>
        {
            the_first_name = string.Empty;
            the_last_name = "Last name";
            the_user_name = "User name";
        };

        Because of = () => the_exception = Catch.Exception(() => subject.CreateProfile(the_user_name, the_first_name, the_last_name));

        It should_throw_a_precondition_exception_containing_the_error = () => the_exception.ShouldBe(typeof(PreconditionException));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_create_a_profile_without_specifying_a_last_name : specification_for_profile_tasks
    {
        static string the_first_name;
        static string the_last_name;
        static string the_user_name;
        static Exception the_exception;

        Establish context = () =>
        {
            the_first_name = "First name";
            the_last_name = string.Empty;
            the_user_name = "User name";
        };

        Because of = () => the_exception = Catch.Exception(() => subject.CreateProfile(the_user_name, the_first_name, the_last_name));

        It should_throw_a_precondition_exception_containing_the_error = () => the_exception.ShouldBe(typeof(PreconditionException));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_create_a_profile_with_a_user_name_that_already_has_a_profile : specification_for_profile_tasks
    {
        static string the_first_name;
        static string the_last_name;
        static string the_user_name;
        static Profile the_profile;
        static Exception the_unique_constraint_exception;
        static Exception the_exception;

        Establish context = () =>
        {
            the_first_name = "First name";
            the_last_name = "Last name";
            the_user_name = "User name";

            the_unique_constraint_exception = new Exception();

            the_profile_repository.Stub(x => x.Save(null)).IgnoreArguments().Throw(the_unique_constraint_exception);
        };

        Because of = () => the_exception = Catch.Exception(() => subject.CreateProfile(the_user_name, the_first_name, the_last_name));
        
        It should_ask_the_profile_repository_to_save_the_new_profile = () =>
            the_profile_repository.AssertWasCalled(x => x.Save(null), o => o.IgnoreArguments());

        It should_catch_the_exception_thrown_by_the_repository = () =>
            the_exception.ShouldNotEqual(the_unique_constraint_exception);

        It should_throw_a_postcondition_exception_containing_the_error = () => the_exception.ShouldBe(typeof(PostconditionException));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_delete_a_profile : specification_for_profile_tasks
    {
        static Profile the_profile;
        static string the_user_id;
        static ProfileByUserNameSpecification dummy_specification;

        Establish context = () =>
            {
                the_profile = new Profile();

                the_user_id = "user_id";

                the_profile_repository.StubFindOne().Return(the_profile);
            };

        Because of = () => subject.DeleteProfile(the_user_id);

        It should_ask_the_profile_repository_for_the_users_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByUserNameSpecification, Profile>(spec => spec.UserName == the_user_id);

        It should_ask_the_profile_repository_to_delete_the_profie = () => the_profile_repository.AssertWasCalled(p => p.Delete(the_profile));
    }

    [Subject(typeof(ProfileTasks))]
    public class when_the_profile_tasks_is_asked_to_delete_a_profile_for_a_user_name_that_does_not_have_a_profile : specification_for_profile_tasks
    {
        static string the_user_id;

        Establish context = () =>
        {
            the_user_id = "user_id";

            the_profile_repository.StubFindOne().Return(null);
        };

        Because of = () => subject.DeleteProfile(the_user_id);

        It should_ask_the_profile_repository_for_the_users_profile = () => the_profile_repository.AssertFindOneWasCalledWithSpecification<ProfileByUserNameSpecification, Profile>(spec => spec.UserName == the_user_id);

        It should_not_try_and_delete_anything = () => the_profile_repository.AssertWasNotCalled(p => p.Delete(new Profile()), m => m.IgnoreArguments());
    }
}