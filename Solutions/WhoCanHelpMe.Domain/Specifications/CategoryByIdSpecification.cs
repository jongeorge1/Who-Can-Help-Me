namespace WhoCanHelpMe.Domain.Specifications
{
    #region Using Directives

    using System;

    using WhoCanHelpMe.Domain;

    #endregion

    public class CategoryByIdSpecification :  QuerySpecification<Category>
    {
        private readonly int categoryId;

        public CategoryByIdSpecification(int categoryId)
        {
            this.categoryId = categoryId;
        }
        
        public int Id
        {
            get { return this.categoryId; }
        }

        public override System.Linq.Expressions.Expression<Func<Category, bool>> MatchingCriteria
        {
            get { return c => c.Id == this.categoryId; }
        }

    }
}