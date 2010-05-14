namespace MSpecTests.WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Web.Controllers.Tag;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_tag_controller : Specification<TagController>
    {
        protected static ITagTasks tag_tasks;

        Establish context = () =>
            {
                tag_tasks = DependencyOf<ITagTasks>();

                ServiceLocatorHelper.AddCachingService();
            };
    }

    [Subject(typeof(TagController))]
    public class when_the_tag_controller_is_asked_for_tags_starting_with_a_string : specification_for_tag_controller
    {
        static ActionResult result;
        static IList<Tag> the_matching_tags;
        static string the_starting_characters;

        Establish context = () =>
            {
                the_starting_characters = "starting";

                the_matching_tags = new List<Tag> {
                                                      new Tag { Name = "Tag1" },
                                                      new Tag { Name = "Tag2" }
                                                  };

                tag_tasks.Stub(tt => tt.GetWhereNameStartsWith(the_starting_characters)).Return(the_matching_tags);
            };

        Because of = () => result = subject.StartingWith(the_starting_characters);

        It should_return_a_content_result =
            () => result.ShouldBe(typeof(ContentResult));

        It should_ask_the_tag_tasks_for_the_matching_tags =
            () => tag_tasks.AssertWasCalled(tt => tt.GetWhereNameStartsWith(the_starting_characters));

        It should_set_the_content_result_data_to_the_list_of_matching_tags =
            () => (result as ContentResult).Content.ShouldEqual("Tag1\nTag2");
    }
}