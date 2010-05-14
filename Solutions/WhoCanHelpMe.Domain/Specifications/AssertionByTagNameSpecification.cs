namespace WhoCanHelpMe.Domain.Specifications
{
    public class AssertionByTagNameSpecification : QuerySpecification<Assertion>
    {
        private readonly string tagName;

        public AssertionByTagNameSpecification(string tagName)
        {
            this.tagName = tagName;
        }

        public string TagName
        {
            get { return this.tagName; }
        }

        public override System.Linq.Expressions.Expression<System.Func<Assertion, bool>> MatchingCriteria
        {
            get { return t => t.Tag.Name == this.tagName; }
        }
    }
}
