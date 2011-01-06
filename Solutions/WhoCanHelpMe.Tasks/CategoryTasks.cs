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

    public class CategoryTasks : ICategoryTasks
    {
        private readonly ILinqRepository<Category> categoryRepository;

        public CategoryTasks(ILinqRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IList<Category> GetAll()
        {
            return this.categoryRepository.FindAll().ToList();
        }

        public Category Get(int categoryId)
        {
            return this.categoryRepository.FindOne(new CategoryByIdSpecification(categoryId));
        }
    }
}
