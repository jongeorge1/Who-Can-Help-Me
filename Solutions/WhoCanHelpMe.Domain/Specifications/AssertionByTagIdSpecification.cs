namespace WhoCanHelpMe.Domain.Specifications
{
    public class AssertionByTagIdSpecification : QuerySpecification<Assertion>
    {
        private readonly int tagId;

        public AssertionByTagIdSpecification(int tagId)
        {
            this.tagId = tagId;
        }
        
        public int TagId
        {
            get { return this.tagId; }
        }

        public override System.Linq.Expressions.Expression<System.Func<Assertion, bool>> MatchingCriteria
        {
            get { return t => t.Tag.Id == this.tagId; }
        }
    }
}
