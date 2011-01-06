namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using Domain;
    using Domain.Contracts.Tasks;
    using Domain.Specifications;

    using SharpArch.Futures.Core.PersistanceSupport;
    using SharpArch.Futures.Core.Specifications;

    #endregion

    public class TagTasks : ITagTasks
    {
        private readonly ILinqRepository<Tag> tagRepository;

        public TagTasks(ILinqRepository<Tag> tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public IList<Tag> GetWhereNameStartsWith(string characters)
        {
            if (string.IsNullOrEmpty(characters))
            {
                return new List<Tag>();
            }

            return this.DoSearch(new TagByFirstCharactersOfNameSpecification(characters));
        }

        public IList<Tag> GetMostPopularTags(int count)
        {
            return this.tagRepository
                       .FindAll()
                       .OrderByDescending(tag => tag.Views)
                       .Take(count)
                       .ToList();
        }

        public Tag GetByName(string name)
        {
            return this.DoSearch(new TagByNameSpecification(name))
                       .FirstOrDefault();
        }

        private IList<Tag> DoSearch(QuerySpecification<Tag> specification)
        {
            return this.tagRepository
                       .FindAll(specification)
                       .OrderBy(t => t.Name)
                       .ToList();
        }
    }
}
