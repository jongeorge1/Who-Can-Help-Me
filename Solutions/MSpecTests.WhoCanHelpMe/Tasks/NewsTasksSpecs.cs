namespace MSpecTests.WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;
    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Services;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Tasks;
    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_news_tasks : Specification<INewsTasks, NewsTasks>
    {
        protected static INewsService the_news_service;

        Establish context = () => the_news_service = DependencyOf<INewsService>();
    }

    [Subject(typeof(NewsTasks))]
    public class when_the_news_tasks_are_asked_to_get_headlines : specification_for_news_tasks
    {
        protected static IList<NewsItem> result;
        static IList<NewsItem> the_stories;

        Establish context = () =>
        {
            the_stories = new List<NewsItem> { new NewsItem(), new NewsItem(), new NewsItem() };

            the_news_service.Stub(r => r.GetHeadlines()).Return(the_stories);
        };

        Because of = () => result = subject.GetProjectBuzz();

        It should_return_the_list_of_headlines = () => result.ShouldContainOnly(the_stories);
    }
}