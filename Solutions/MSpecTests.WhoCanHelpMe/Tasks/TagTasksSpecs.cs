namespace MSpecTests.WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Repositories;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Domain.Specifications;
    using global::WhoCanHelpMe.Tasks;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Machine.Specifications.Runner.Impl;

    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_tag_tasks : Specification<ITagTasks, TagTasks>
    {
        protected static ITagRepository the_tag_repository;

        Establish context = () =>
        {
            the_tag_repository = DependencyOf<ITagRepository>();
        };
    }

    public class when_the_tag_tasks_are_asked_to_get_tags_matching_starting_characters_but_no_characters_are_supplied : specification_for_tag_tasks
    {
        static string the_starting_characters;
        static IList<Tag> result;

        Establish context = () => the_starting_characters = string.Empty;

        Because of = () => result = subject.GetWhereNameStartsWith(the_starting_characters);

        It should_not_ask_the_repository_for_matching_tags = () => the_tag_repository.AssertFindAllWasNotCalledWithSpecification<TagByFirstCharactersOfNameSpecification, Tag>();

        It should_return_an_empty_list = () => result.ShouldBeEmpty();
    }

    public class when_the_tag_tasks_are_asked_to_get_tags_matching_starting_characters_and_there_are_matching_tags : specification_for_tag_tasks
    {
        static string the_starting_characters;
        static IQueryable<Tag> the_matching_tags;
        static IList<Tag> result;

        Establish context = () =>
        {
            the_starting_characters = "ab";

            the_matching_tags = new List<Tag> {
                                                      new Tag { Name = "X" },
                                                      new Tag { Name = "A" },
                                                      new Tag { Name = "M" },
                                                  }.AsQueryable();

            the_tag_repository.StubFindAll().Return(the_matching_tags);
        };

        Because of = () => result = subject.GetWhereNameStartsWith(the_starting_characters);

        It should_ask_the_tag_repository_for_the_matching_tags = () => the_tag_repository.AssertFindAllWasCalledWithSpecification<TagByFirstCharactersOfNameSpecification, Tag>(s => s.StartingCharacters == the_starting_characters);

        It should_return_the_list_of_matching_tags = () => the_matching_tags.ForEach(m => result.ShouldContain(m)); // ShouldContainOnly doesn't work - the assertion is badly named, as it is broken by different orderings of elements

        It should_sort_the_matching_tags_alphabetically = () =>
        {
            for (int index = 1; index < result.Count; index++)
            {
                result[index - 1].Name.ShouldBeLessThan(result[index].Name);
            }
        };
    }

    public class when_the_tag_tasks_are_asked_to_get_tags_matching_starting_characters_and_there_are_no_matching_tags : specification_for_tag_tasks
    {
        static string the_starting_characters;
        static IQueryable<Tag> the_matching_tags;
        static IList<Tag> result;

        Establish context = () =>
        {
            the_starting_characters = "ab";

            the_matching_tags = new List<Tag>().AsQueryable();

            the_tag_repository.StubFindAll().Return(the_matching_tags);
        };

        Because of = () => result = subject.GetWhereNameStartsWith(the_starting_characters);

        It should_ask_the_tag_repository_for_the_matching_tags = () => the_tag_repository.AssertFindAllWasCalledWithSpecification<TagByFirstCharactersOfNameSpecification, Tag>(s => s.StartingCharacters == the_starting_characters);

        It should_return_an_empty_list = () => result.ShouldBeEmpty();
    }

    public class when_the_tag_tasks_are_asked_for_the_most_popular_tags : specification_for_tag_tasks
    {
        static int the_tag_count;
        static IQueryable<Tag> the_matching_tags;
        static IList<Tag> result;

        Establish context = () =>
        {
            the_tag_count = 10;

            the_matching_tags = new List<Tag> {
                                                  new Tag { Name = "X" },
                                                  new Tag { Name = "A" },
                                                  new Tag { Name = "M" },
                                              }.AsQueryable();

            the_tag_repository.Stub(r => r.PopularTags(the_tag_count)).Return(the_matching_tags);
        };

        Because of = () => result = subject.GetMostPopularTags(the_tag_count);

        It should_ask_the_tag_repository_for_the_popular_tags = () => the_tag_repository.AssertWasCalled(r => r.PopularTags(the_tag_count));

        It should_return_the_list_of_tags = () => result.ShouldContainOnly(the_matching_tags);
    }

    public class when_the_tag_tasks_are_asked_to_get_a_tag_by_name : specification_for_tag_tasks
    {
        static Tag result;
        static Tag the_tag;
        static string the_tag_name;

        Establish context = () =>
            {
                the_tag_name = "tag name";

                the_tag = new Tag();

                the_tag_repository.StubFindAll().Return(new List<Tag>{the_tag}.AsQueryable());
            };

        Because of = () => result = subject.GetByName(the_tag_name);

        It should_ask_the_tag_repository_for_the_matching_tag = () => the_tag_repository.AssertFindAllWasCalledWithSpecification<TagByNameSpecification, Tag>(s => s.Name == the_tag_name);

        It should_return_the_matching_tag = () => result.ShouldBeTheSameAs(the_tag);

    }

    public class when_the_tag_tasks_are_asked_to_get_a_tag_by_name_and_there_is_no_matching_tag : specification_for_tag_tasks
    {
        static Tag result;
        static string the_tag_name;

        Establish context = () =>
        {
            the_tag_name = "tag name";

            the_tag_repository.StubFindAll().Return(new List<Tag>().AsQueryable());
        };

        Because of = () => result = subject.GetByName(the_tag_name);

        It should_ask_the_tag_repository_for_the_matching_tag = () => the_tag_repository.AssertFindAllWasCalledWithSpecification<TagByNameSpecification, Tag>(s => s.Name == the_tag_name);

        It should_return_null = () => result.ShouldBeNull();
    }
}
