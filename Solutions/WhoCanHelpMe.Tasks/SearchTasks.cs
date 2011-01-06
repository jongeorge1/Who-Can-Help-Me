namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using Domain;
    using Domain.Contracts.Tasks;
    using Domain.Specifications;

    using SharpArch.Futures.Core.PersistanceSupport;

    #endregion

    public class SearchTasks : ISearchTasks
    {
        private readonly ILinqRepository<Assertion> assertionRepository;

        private readonly ILinqRepository<Tag> tagRepository;

        public SearchTasks(
            ILinqRepository<Assertion> assertionRepository,
            ILinqRepository<Tag> tagRepository)
        {
            this.assertionRepository = assertionRepository;
            this.tagRepository = tagRepository;
        }

        public IList<Assertion> ByTag(string tagName)
        {
            // Find the tag in question
            var tag = this.tagRepository.FindOne(new TagByNameSpecification(tagName));

            if (tag != null)
            {
                // Update the view count for the tag
                tag.Views++;
                this.tagRepository.Save(tag);

                // Find and return matching assertions
                return this.assertionRepository
                           .FindAll(new AssertionByTagIdSpecification(tag.Id))
                           .OrderBy(o => o.Profile.LastName)
                           .ToList();
            }

            return new List<Assertion>(0);
        }
    }
}
