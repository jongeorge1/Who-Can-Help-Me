namespace WhoCanHelpMe.Domain.Specifications
{
    using SharpArch.Futures.Core.Specifications;

    public class AssertionByCategoryIdSpecification : QuerySpecification<Assertion>
    {
        private readonly int categoryId;

        public AssertionByCategoryIdSpecification(int categoryId)
        {
            this.categoryId = categoryId;
        }

        public override System.Linq.Expressions.Expression<System.Func<Assertion, bool>> MatchingCriteria
        {
            get { return t => t.Category.Id == this.categoryId; }
        }
    }
}
